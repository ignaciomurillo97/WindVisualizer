Coordenadas c;
void setup(){
  size(800,600);
  c = new Coordenadas(300);
  background(0);
}
void draw(){
  background(0);
  pushMatrix();
  translate(width/2, height/2);
  for(int lat = -90; lat < 90; lat++){
    for(int lon = -180; lon < 180; lon++){
      PVector punto = c.GEOtoXYZ(lat,lon);
      fill(255);
      ellipse(punto.x, punto.y,5,5);
    }
  }
  //center
  fill(255,0,0);
  ellipse(0,0,5,5);
  popMatrix();
  
  PVector gg = new PVector(0,300,0);
  PVector dd = c.XYZtoGEO(gg);
  print("lat: "+dd.x+" long:"+dd.y+" ");
}