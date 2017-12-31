import controlP5.*;

Globe g;


ControlP5 cp5;

void setup(){
  size(800, 600, P3D);
  background(51);
  g = new Globe(width / 2, height / 2, 10, 200);
  g.yRot = PI;
  cp5 = new ControlP5(this);
  initContorls();
}

void draw(){
  background(51);
  g.update();
  g.display();
  testCoordenadas();
}

void testCoordenadas(){
  pushMatrix();
  PVector p = g.c.GEOtoXYZ(radians(9.934739), radians(-84.087502));
  strokeWeight(15);
  stroke(255);
  translate(width / 2, height / 2);
  rotateY(PI);
  point(p.x, p.y, p.z);
  popMatrix();
}

void loadWindData(){
  JSONObject json = loadJSONObject("result.json");
  JSONArray values = json.getJSONArray("data");

  for (int i = 0; i < values.size(); i++){
    JSONObject dataPoint = values.getJSONObject(i);

    float vel = dataPoint.getFloat("speed");
    float ang = dataPoint.getFloat("deg");
    float lat = dataPoint.getFloat("lat");
    float lon = dataPoint.getFloat("lon");

    WindDataObj windObj = new WindDataObj(vel, ang, lat, lon);
  }
}

void initContorls(){
}
