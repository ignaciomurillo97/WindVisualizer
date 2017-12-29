Globe g;
void setup(){
  size(800, 600);
  background(51);
  g = new Globe(width / 2, height / 2, 10, 10);
}

void draw(){
  g.display();
}

