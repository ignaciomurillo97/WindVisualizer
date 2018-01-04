import controlP5.*;

Globe globe;
FlowField flowField;

ControlP5 cp5;

float rotY, rotZ;

void setup(){
  size(800, 600, P3D);
  background(51);
  ambientLight(47, 252, 201);
  globe = new Globe(width / 2, height / 2, 10, 200);
  cp5 = new ControlP5(this);
  loadWindData();
  initContorls();
}

void draw(){
  background(51);
  globe.update();
  globe.display();
  globe.showFlowField();
  rotation();
}

void loadWindData(){
  JSONObject json = loadJSONObject("result.json");
  JSONArray values = json.getJSONArray("data");
  float latStep = json.getFloat("latStep");
  float lonStep = json.getFloat("lonStep");
  int latSamples = json.getInt("latSamples");
  int lonSamples = json.getInt("lonSamples");
  ArrayList<WindDataObj> windDataList = new ArrayList();
  boolean success = true;

  for (int i = 0; i < values.size(); i++){
    JSONObject dataPoint = values.getJSONObject(i);
    
    float vel = 0;
    float ang = 0;
    float lat = 0;
    float lon = 0;

    try {
      vel = dataPoint.getFloat("speed");
      ang = dataPoint.getFloat("deg");

      lat = dataPoint.getFloat("lat");
      lon = dataPoint.getFloat("lon");
    } catch (Exception e) {
      print(e);
      print(dataPoint.toString());
      success = false;
    }

    WindDataObj windObj = new WindDataObj(vel, ang, lat, lon);
    windDataList.add(windObj);
  }

  if (success){
    WindDataObj[] windDataArray = windDataList.toArray(new WindDataObj[windDataList.size()]);
    flowField = new FlowField(latStep, lonStep, latSamples, lonSamples, windDataArray);
  }
}

void rotation (){
  if (mousePressed) {
    rotZ = (height/2 - mouseY)*PI/(width/2);
    rotY = mouseX*PI/(height/2);
  }
}

void initContorls(){
}
