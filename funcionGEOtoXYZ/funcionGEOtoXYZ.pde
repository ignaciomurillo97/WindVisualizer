Coordenadas c;
void setup(){
  size(600,600);
  background(0);
}
void draw(){
  c = new Coordenadas(300);
  background(0);
  noStroke();
  pushMatrix();
  translate(width/2, height/2);
  for(int lat = -90; lat < 90; lat++){
    for(int lon = -180; lon < 180; lon+=15){
      PVector punto = c.GEOtoXYZ(lat,lon);
      PVector representacion = c.isometriaAngulo(punto.x, punto.z, punto.y, 0);
      if(lat == 0 && lon ==0){
        fill(255,0,0);
        ellipse(representacion.x, representacion.y,5,5);
      }else 
      if(lat == 89 && lon ==179){
        fill(255,0,255);
        ellipse(representacion.x, representacion.y,5,5);
      }else {
        fill(255);
        ellipse(representacion.x, representacion.y,1,2);
      }
    }
  }
  //center
  fill(255,0,0);
  ellipse(0,0,5,5);
  popMatrix();
}