#include <ESP8266WiFi.h>
#ifndef APSSID
#define APSSID "MyCar"
#define APPSK  "12345678"
#endif

const char *ssid = APSSID;
const char *password = APPSK;
WiFiServer server(23);
boolean check = false;

// initilize outputs poins
int up=D1;
int down=D2;
int turnLeft=D3;
int turnRight=D4;

void SetupWifi()
{
  Serial.begin(115200);
  delay(10);
  Serial.println();
  Serial.println();
  Serial.print("Waiting for connection  ");
  Serial.println(ssid);
  WiFi.softAP(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  IPAddress myIP = WiFi.softAPIP();
  Serial.print("AP IP address: ");
  Serial.println(myIP);
  
  server.begin();
  Serial.println("telnet server started");
}
void Move(char moveCammand)
{
    if (moveCammand == '1') GoUp();   
    if (moveCammand == '2')GoBack();
    if (moveCammand == '3')TurnLeft();
    if (moveCammand == '4')TurnRight();
    if (moveCammand == '5')GoLeftUp();
    if (moveCammand == '6')GoRightUp();
    if (moveCammand == '7')BackLeft();
    if (moveCammand == '8')BackRight();
    if (moveCammand == '0')Stop();
}
void Stop()
{
    digitalWrite(down,LOW);
    digitalWrite(turnLeft,LOW);
    digitalWrite(turnRight,LOW);
    digitalWrite(down,LOW);
    digitalWrite(up,LOW);
}
void GoUp(){
    digitalWrite(turnLeft,LOW);
    digitalWrite(turnRight,LOW);
    digitalWrite(down,LOW);
    digitalWrite(up,HIGH);
}
void GoBack()
{
     digitalWrite(turnLeft,LOW);
     digitalWrite(turnRight,LOW);
     digitalWrite(down,HIGH);
     digitalWrite(up,LOW);
}
void TurnLeft()
{
     digitalWrite(turnLeft,HIGH);
     digitalWrite(turnRight,LOW);
     digitalWrite(down,LOW);
     digitalWrite(up,LOW);
}
 void TurnRight()
{
     digitalWrite(turnLeft,LOW);
     digitalWrite(turnRight,HIGH);
     digitalWrite(down,LOW);
     digitalWrite(up,LOW);
}
 void GoLeftUp()
{
     digitalWrite(turnLeft,HIGH);
     digitalWrite(turnRight,LOW);
     digitalWrite(down,LOW);
     digitalWrite(up,HIGH);
}
 void GoRightUp()
{          
    digitalWrite(turnLeft,LOW);
    digitalWrite(turnRight,HIGH);
    digitalWrite(down,LOW);
    digitalWrite(up,HIGH);
}
 
void BackLeft()
{
    digitalWrite(turnLeft,HIGH);
    digitalWrite(turnRight,LOW);
    digitalWrite(down,HIGH);
    digitalWrite(up,LOW);
}
void BackRight()
{  
    digitalWrite(turnLeft,LOW);
    digitalWrite(turnRight,HIGH);
    digitalWrite(down,HIGH);
    digitalWrite(up,LOW);
}
void setup() {
  //initlize pinze
  pinMode(up, OUTPUT);
  pinMode(down, OUTPUT); 
  pinMode(turnLeft, OUTPUT); 
  pinMode(turnRight, OUTPUT); 
  
  Stop();
  delay(1000);
  SetupWifi();

}
void loop() {
  WiFiClient client = server.available();
  if (client)
  {
    Serial.println("\n[Client connected]");
    if(!check)
    {
      client.flush();
      Serial.println("New Client...");
      client.println("Hello....");
      check = true;
    }
    while (client.connected())
    {
      // read line by line what the client (web browser) is requesting
      if (client.available())
      {
        char moveCammand = client.read();
        Move(moveCammand);
        Serial.print(moveCammand);
      }
    }
    delay(1); // give the web browser time to receive the data

    // close the connection:
    //client.stop();
    //Serial.println("[Client disonnected]");
  }

}
