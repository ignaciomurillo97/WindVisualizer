#!/bin/python3

# Pide los datos del viento de OpenWindApi, cada request se realiza con un 
# segundo de delay para no sobrepasar el maximo de requests por minuto (60)
# Para cambiar la cantidad de muestras que toma en latitudes y longitudes solo 
# hace falta cambiar "latSamples" y "lonSamples".

import requests
import sys
import json
from time import *

latSamples = 10
lonSamples = 10
outputFileName = "result.json"

sampleList = []

def main():
    getSamples()
    jsonRes = json.dumps(sampleList)
    outputFile = open(outputFileName, 'w')
    outputFile.write(jsonRes)

def getSamples():
    latStep = 180 / latSamples
    lonStep = 360 / lonSamples
    maxSamples = latSamples * lonSamples
    for i in range(latSamples):
        for j in range(lonSamples):
            lat = j * latStep - 90
            lon = i * lonStep - 180
            windData = getWindAt(lat, lon)
            if (windData):
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
    sys.stdout.write('[%s] %s%s ...%s\r' % (bar, percents, '%', status))
    sys.stdout.flush()

if (__name__ == '__main__'):
    main();
