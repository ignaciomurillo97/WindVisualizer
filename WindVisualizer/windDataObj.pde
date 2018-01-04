class WindDataObj{

  float vel;
  float ang;
  float lat;
  float lon;
  PVector windVector;

  WindDataObj(float vel, float ang, float lat, float lon){
    this.vel = vel;
    this.ang = ang;
    this.lat = lat;
    this.lon = lon;

    this.windVector = PVector.fromAngle(radians(ang));
    this.windVector.mult(vel);
  }

}

