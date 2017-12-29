class Icosphere{
  PVector rotation;
  PVector location;
  ArrayList<PVector> vertices;
  ArrayList<TriangleIndices> triangles;

  Icosphere(PVector location, PVector rotation){
    this.rotation = rotation;
    this.location = location;

    vertices = new ArrayList<PVector>();
    triangles = new ArrayList<TriangleIndices>();
    generateVertices();
    generateTriangles();
  }
  
  void generateVertices(){
    float t = (1.0 + sqrt(5.0)) / 2.0;

    vertices.add(new PVector(-1,  t,  0));
    vertices.add(new PVector( 1,  t,  0));
    vertices.add(new PVector(-1, -t,  0));
    vertices.add(new PVector( 1, -t,  0));

    vertices.add(new PVector( 0, -1,  t));
    vertices.add(new PVector( 0,  1,  t));
    vertices.add(new PVector( 0, -1, -t));
    vertices.add(new PVector( 0,  1, -t));

    vertices.add(new PVector( t,  0, -1));
    vertices.add(new PVector( t,  0,  1));
    vertices.add(new PVector(-t,  0, -1));
    vertices.add(new PVector(-t,  0,  1));
  }

  void generateTriangles(){
    // 5 faces around point 0
    triangles.add(new TriangleIndices(0, 11, 5));
    triangles.add(new TriangleIndices(0, 5, 1));
    triangles.add(new TriangleIndices(0, 1, 7));
    triangles.add(new TriangleIndices(0, 7, 10));
    triangles.add(new TriangleIndices(0, 10, 11));

    // 5 adjacent faces
    triangles.add(new TriangleIndices(1, 5, 9));
    triangles.add(new TriangleIndices(5, 11, 4));
    triangles.add(new TriangleIndices(11, 10, 2));
    triangles.add(new TriangleIndices(10, 7, 6));
    triangles.add(new TriangleIndices(7, 1, 8));

    // 5 faces around point 3
    triangles.add(new TriangleIndices(3, 9, 4));
    triangles.add(new TriangleIndices(3, 4, 2));
    triangles.add(new TriangleIndices(3, 2, 6));
    triangles.add(new TriangleIndices(3, 6, 8));
    triangles.add(new TriangleIndices(3, 8, 9));

    // 5 adjacent faces
    triangles.add(new TriangleIndices(4, 9, 5));
    triangles.add(new TriangleIndices(2, 4, 11));
    triangles.add(new TriangleIndices(6, 2, 10));
    triangles.add(new TriangleIndices(8, 6, 7));
    triangles.add(new TriangleIndices(9, 8, 1));
  }

  void setRadius (float radius){
    for (PVector v : vertices){
      v.normalize();
      v.mult(radius);
    }
  }

  void displayVerticesWithNoise(){
    float dispX = random(100);
    float dispY = random(100);
    float dispZ = random(100);

    pushMatrix();
    stroke(255);
    translate(location.x, location.y, location.z);
    strokeWeight(2);
    rotateY(rotation.y);
    for (PVector v : vertices){
      v.normalize();
      v.mult(noise(v.x/2 + dispX, v.y/2 + dispY, v.z/2 + dispZ) * 250);
      point(v.x, v.y, v.z);
    }
    popMatrix();
  }

  void displayVertices(){
    pushMatrix();
    stroke(255);
    translate(location.x, location.y, location.z);
    strokeWeight(2);
    rotateY(rotation.y);
    for (PVector v : vertices){
      point(v.x, v.y, v.z);
    }
    popMatrix();
  }

  void displayEdges(){
    pushMatrix();
    stroke(255);
    strokeWeight(2);
    translate(location.x, location.y, location.z);
    rotateY(rotation.y);
    for (TriangleIndices t : triangles){
      PVector pointA = vertices.get(t.a);
      PVector pointB = vertices.get(t.b);
      PVector pointC = vertices.get(t.c);
      line(pointA.x, pointA.y, pointA.z, pointB.x, pointB.y, pointB.z);
      line(pointC.x, pointC.y, pointC.z, pointB.x, pointB.y, pointB.z);
      line(pointA.x, pointA.y, pointA.z, pointC.x, pointC.y, pointC.z);
    }
    popMatrix();
  }

  void subdivide(){
    ArrayList<PVector> newPoints = new ArrayList<PVector>();
    ArrayList<TriangleIndices> newTriangles = new ArrayList<TriangleIndices>();

    for (TriangleIndices t : triangles){
      PVector pointA = vertices.get(t.a);
      PVector pointB = vertices.get(t.b);
      PVector pointC = vertices.get(t.c);

      PVector pointAB  = PVector.add(pointA, pointB).div(2);
      PVector pointBC  = PVector.add(pointB, pointC).div(2);
      PVector pointCA  = PVector.add(pointC, pointA).div(2);

      int startingIndex = newPoints.size();

      newPoints.add(pointA); // + 0
      newPoints.add(pointB); // + 1
      newPoints.add(pointC); // + 2

      newPoints.add(pointAB); // + 3
      newPoints.add(pointBC); // + 4
      newPoints.add(pointCA); // + 5

      newTriangles.add(new TriangleIndices(startingIndex + 0, startingIndex + 3, startingIndex + 5));
      newTriangles.add(new TriangleIndices(startingIndex + 1, startingIndex + 3, startingIndex + 4));
      newTriangles.add(new TriangleIndices(startingIndex + 2, startingIndex + 4, startingIndex + 5));
      newTriangles.add(new TriangleIndices(startingIndex + 3, startingIndex + 4, startingIndex + 5));
    }

    vertices = newPoints;
    triangles = newTriangles;
  }
}
