class Coordenadas {
  
  float radio;
  
  Coordenadas(float radio){
    this.radio = radio;
  }
  
  PVector GEOtoXYZ(float latitud, float longitud) {
    float hr = cos(-latitud*(PI/180))*radio;
    PVector cordCartesianas = 
      new PVector (
        sin(longitud*(PI/180))*hr, 
        sin(-latitud*(PI/180))*radio, 
        cos(longitud*(PI/180))*hr
      );
    return cordCartesianas;
  }


  PVector XYZtoGEO(PVector cartesiano) {
    PVector Geo = new PVector(
    //lat
    asin(cartesiano.y/radio)/(PI/180)
    ,//long 
    asin(cartesiano.x/cartesiano.z)/(PI/180)
    );
    return Geo;
  }
  
}