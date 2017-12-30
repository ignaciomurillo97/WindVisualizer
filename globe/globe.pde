PImage earth;
PShape globe;
Coordenadas c;

void setup(){
  size(800, 600, P3D);
  background(0);
  earth = loadImage("https://upload.wikimedia.org/wikipedia/commons/2/26/PathfinderMap_hires_%284996917742%29.jpg");
  c = new Coordenadas(200);
  globe = createShape(SPHERE, 200);
  globe.setTexture(earth);
  globe.setStroke(false);
}

void draw(){
  pushMatrix();
  background(51);
  noStroke();
  translate(width / 2, height / 2);
  rotateX(-PI / 6);
  rotateY(frameCount * -0.01);
  shape(globe);
  popMatrix();
  pruebaCoordenadas();
}

void pruebaCoordenadas(){
  pushMatrix();
  PVector latlon = new PVector(9.934739, -84.087502);
  PVector cart = c.GEOtoXYZ(latlon.x, latlon.y);
  translate(width / 2, height / 2);
  rotateX(-PI / 6);
  rotateY(frameCount * -0.01);
  strokeWeight(5);
  stroke(255);
  point(cart.x, cart.y, cart.z);
  popMatrix();
}
