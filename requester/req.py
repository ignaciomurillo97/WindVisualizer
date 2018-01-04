#!/bin/python3

# Pide los datos del viento de OpenWindApi, cada request se realiza con un 
# segundo de delay para no sobrepasar el maximo de requests por minuto (60)
# Para cambiar la cantidad de muestras que toma en latitudes y longitudes solo 
# hace falta cambiar "latSamples" y "lonSamples".

import requests
import json
from time import *
from sys import *

params = argv[1:]

latSamples = 5
lonSamples = 5

if (len(params)):
    latSamples = int(params[0])
    lonSamples = int(params[1])

latStep = 180 / latSamples
lonStep = 360 / lonSamples

outputFileName = "result.json"

sampleList = []

def main():
    getSamples()
    resDict = {"data": sampleList}

    resDict["lonStep"] = lonStep;
    resDict["latStep"] = latStep;

    resDict["latSamples"] = latSamples;
    resDict["lonSamples"] = lonSamples;

    jsonRes = json.dumps(resDict)
    outputFile = open(outputFileName, 'w')
    outputFile.write(jsonRes)

def getSamples():
    maxSamples = latSamples * lonSamples
    for i in range(latSamples):
        for j in range(lonSamples):
            lat = j * latStep - 90
            lon = i * lonStep - 180
            windData = getWindAt(lat, lon)
            if (windData and windData["speed"] and windData["deg"]):
                windData["lat"] = lat
                windData["lon"] = lon
                sampleList.append(windData);
            else:
                print("failed at: " + str((lat, lon)))
            sleep(1)
            progress(i * latSamples + j, maxSamples)

def getWindAt(lat, lon):
    r = requests.get(buildUrl(lat, lon))
    resData = json.loads(r.text)
    if(resData["cod"] == 200):
        windData = resData["wind"]
        return resData["wind"]

def buildUrl(lat, lon):
    url = "http://api.openweathermap.org/data/2.5/weather?lat="
    url += str(lat)+"&lon="+ str(lon)
    url += "&APPID=d5269ad59d408c881bd47821a970ab87"
    return url 

def progress(count, total, status=''):
    barLen = 60
    filledLen = int(round(barLen * count / float(total)))
    percents = round(100.0 * count / float(total), 1)
    bar = '=' * filledLen + '-' * (barLen - filledLen)
    stdout.write('[%s] %s%s ...%s\r' % (bar, percents, '%', status))
    stdout.flush()

if (__name__ == '__main__'):
    main();
