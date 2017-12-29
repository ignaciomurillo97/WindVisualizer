PImage earth;
PShape globe;

void setup(){
  size(800, 600, P3D);
  background(0);
  earth = loadImage("https://upload.wikimedia.org/wikipedia/commons/2/26/PathfinderMap_hires_%284996917742%29.jpg");
  globe = createShape(SPHERE, 200);
  globe.setTexture(earth);
  globe.setStroke(false);
}

void draw(){
  noStroke();
  translate(width / 2, height / 2);
  rotateX(-PI / 6);
  rotateY(frameCount * -0.01);
  shape(globe);
}
