#include <Servo.h> 
 
Servo myservo;  // create servo object to control a servo 
                // a maximum of eight servo objects can be created 

void setup() 
{ 
  myservo.attach(5);  // attaches the servo on pin 9 to the servo object 
  
} 
 
 
void loop() 
{ 
  int val = analogRead(2)/6;
  myservo.write(val);
  delay(15);                       // waits 15ms for the servo to reach the position 
}
