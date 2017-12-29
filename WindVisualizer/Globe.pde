class Globe{
  PVector pos;
  float radius;

  PImage texture;
  
  Globe(float x, float y, float z, float radius){
    this.pos = PVector(x, y, z);
    this.radius = radius;
  }

  void update(){
    
  }

  void display(){
    translate(pos.x, pos.y, pos.z);
    sphere(radius);
  }

}
