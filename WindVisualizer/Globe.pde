class Globe{
  PImage earth;
  PShape globe;

  PVector pos;
  float radius;
  float yRot;

  PImage texture;
  Coordenadas c;

  Globe(float x, float y, float z, float radius){
    earth = loadImage("https://upload.wikimedia.org/wikipedia/commons/2/26/PathfinderMap_hires_%284996917742%29.jpg");
    globe = createShape(SPHERE, radius);
    globe.setTexture(earth);
    globe.setStroke(false);
    c = new Coordenadas(radius);

    this.pos = new PVector(x, y, z);
    this.radius = radius;
  }

  void update(){
  }

  void display(){
    pushMatrix();
    noStroke();
    translate(width / 2, height / 2);
    rotateY(yRot);
    shape(globe);
    popMatrix();
  }

}
