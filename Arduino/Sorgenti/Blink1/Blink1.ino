/*
  Blink
  Turns on an LED on for one second, then off for one second, repeatedly.

  Most Arduinos have an on-board LED you can control. On the Uno and
  Leonardo, it is attached to digital pin 13. If you're unsure what
  pin the on-board LED is connected to on your Arduino model, check
  the documentation at http://arduino.cc

  This example code is in the public domain.

  modified 8 May 2014
  by Scott Fitzgerald
 */

int contatore;

// the setup function runs once when you press reset or power the board
void setup() {
  // initialize digital pin 13 as an output.
  //pinMode(2, OUTPUT);
  
  Serial.begin(115200);
  Serial.println("OK!");

  contatore=100;
}

// the loop function runs over and over again forever


void loop() {
  delay(100);              // wait for a second

  Serial.println(contatore);
  contatore--;
  
  if ( contatore <= 0 )
  {
    digitalWrite(2, HIGH);   // turn the LED on (HIGH is the voltage level)
    Serial.println("Beeeep!!!!");
    
    delay( 1000 );
    digitalWrite(2, LOW);
    contatore=100;

    Serial.println("Si ricomincia...");
  }
  
  
  int luce = analogRead( A0 );
  Serial.print("Luce: ");
  Serial.println(luce);
}
