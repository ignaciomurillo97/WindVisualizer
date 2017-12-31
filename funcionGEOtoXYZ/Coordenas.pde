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
  
  
  //Funcion de Isometria, retorna un par ordenado dada una triada
    PVector isometriaAngulo(float x, float y, float z){
      float ry = 0.0;
        PVector coordenadas2d = new PVector(y - x, -0.6*(x*ry + y*ry - 2*z));
        return coordenadas2d;
    }

    PVector isometriaAngulo(float x, float y, float z, float ang){
      float angulo = ang*(PI/180);
      float p1 = ((cos(angulo)*x) - (sin(angulo)*y));
      float p2 = (sin(angulo)*x) + (cos(angulo)*y);
      float p3 = z;
        PVector isor = isometriaAngulo(p1 , p2 , p3);
        return isor;
    }
  
}