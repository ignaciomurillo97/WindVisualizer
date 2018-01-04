class Globe{
  PImage earth;
  PShape globe;

  PVector pos;
  float radius;

  PImage texture;

  Coordenadas coord;

  Globe(float x, float y, float z, float radius){
    earth = loadImage("EarthTexture.jpg");
    globe = createShape(SPHERE, radius);
    globe.setTexture(earth);
    globe.setStroke(false);
    coord = new Coordenadas(radius);

    this.pos = new PVector(x, y, z);
    this.radius = radius;
  }

  void update(){
  }

  void display(){
    pushMatrix();
    noStroke();
    adjustMatrix();
    shape(globe);
    popMatrix();
  }

  void showFlowField(){
    WindDataObj[][] windVectors = flowField.windVectors;
    pushMatrix();
    adjustMatrix();
    stroke(255);

    for (int i = 0; i < windVectors.length; i++){
      for (int j = 0; j < windVectors[0].length; j++){
        WindDataObj currWindObj = windVectors[i][j];
        showVector(currWindObj);
      }
    }

    popMatrix();
  }

  void showVector(WindDataObj windData){
    PVector currentVector = windData.windVector.copy();
    PVector vectorPos = coord.GEOtoXYZ(windData.lat, windData.lon);
    PVector latlon = new PVector(windData.lon, windData.lat);

    currentVector.add(latlon);
    PVector otherPos = coord.GEOtoXYZ(currentVector.y, currentVector.x);
    line(vectorPos.x, vectorPos.y, vectorPos.z, otherPos.x, otherPos.y, otherPos.z);
  }

  float radiusMultiplier(float lat){
    return cos(radians(lat)) * radius;
  }

  void adjustMatrix(){
    translate(pos.x, pos.y, pos.z);
    rotateX(rotZ);
    rotateY(rotY);
  }

}
