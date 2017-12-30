class Coordenadas {
  
  float radio;
  
  Coordenadas(float radio){
    this.radio = radio;
  }
  
  PVector GEOtoXYZ(float latitud, float longitud) {
    PVector cordCartesianas = 
    new PVector (sin(latitud)*radio, sin(longitud)*radio, cos(latitud)*radio);
    return cordCartesianas;
  }


  PVector XYZtoGEO(PVector cartesiano) {
    PVector Geo = new PVector(asin(cartesiano.y/radio), atan(cartesiano.x/cartesiano.z));
    return Geo;
  }
  
}