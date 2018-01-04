class FlowField{

  float latStep;
  float lonStep;
  WindDataObj[][] windVectors;

  FlowField(float latStep, float lonStep, int latSamples, int lonSamples, WindDataObj[] windData){
    this.latStep = latStep;
    this.lonStep = lonStep;
    this.windVectors = new WindDataObj[latSamples][lonSamples];
    fillVectors(windData);
  }

  void fillVectors (WindDataObj[] windData){
    for (WindDataObj wd : windData){
      int i = (int)latToIndex(wd.lat);
      int j = (int)lonToIndex(wd.lon);

      windVectors[i][j] = wd;
    }
  }

  float latToIndex(float lat){
    return (int)((lat + 90) / latStep);   
  }

  float lonToIndex(float lon){
    return (int)((lon + 180) / lonStep);
  }

  float indexToLat(float index){
    return index * latStep - 90;
  }

  float indexToLon(float index){
    return index * lonStep - 180;
  }

  PVector velocityAtGeoLocation(float lat, float lon){
    float i = latToIndex(lat);
    float j = lonToIndex(lon);

    int i1 = (int)i; int j1 = (int)j;
    int i2 = i1 + 1; int j2 = j1;
    int i3 = i1    ; int j3 = j1 + 1;
    int i4 = i1 + 1; int j4 = j1 + 1;

    float percentI = i - i1;
    float percentJ = j - j1;

    PVector p1 = windVectors[i1][j1].windVector;
    PVector p2 = windVectors[i2][j2].windVector;
    PVector p3 = windVectors[i3][j3].windVector;
    PVector p4 = windVectors[i4][j4].windVector;

    PVector p12 = PVector.lerp(p1, p2, percentI);
    PVector p34 = PVector.lerp(p3, p4, percentI);

    return PVector.lerp(p12, p34, percentJ);
  }

}
