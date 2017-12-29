Icosphere i;

void setup(){
  size(600, 600, P3D);
  background(51);
  i = new Icosphere(new PVector(width / 2, height / 2, 10), new PVector(0, 0, 0));
  i.subdivide();
  i.subdivide();
  i.subdivide();
  i.subdivide();
  i.displayVerticesWithNoise();
}

void draw(){
}
