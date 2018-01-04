class Coordenadas {
  
  float latOffset;
  float lonOffset;
  
  float radio;
  
  Coordenadas(float radio){
    latOffset = 1.25;
    lonOffset = -0.28;
    this.radio = radio;
  }
  
  PVector GEOtoXYZ(float latitud, float longitud) {
    latitud = radians(latitud + latOffset);
    longitud = radians(longitud + lonOffset);

    float x = radio * sin(longitud) * cos(latitud);
    float z = radio * sin(longitud) * sin(latitud);
    float y = radio * cos(longitud);
    PVector cordCartesianas = 
    new PVector(x, y, z);
    return cordCartesianas;
  }


  PVector XYZtoGEO(PVector cartesiano) {
    PVector Geo = new PVector(asin(cartesiano.y/radio), atan(cartesiano.x/cartesiano.z));
    return Geo;
  }
  
}
