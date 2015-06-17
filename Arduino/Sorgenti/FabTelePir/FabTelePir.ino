#include <IRremote.h>

#define LED 2
#define BOARD 13
#define PIR 3
#define IR 4

IRrecv irrecv(IR);
decode_results results;

bool armed = false;

void setup() {

  // put your setup code here, to run once:
  pinMode( LED, OUTPUT );
  pinMode( BOARD, OUTPUT );
  pinMode( PIR, INPUT );
  pinMode( IR, INPUT );
  
  Serial.begin(9600);
  irrecv.enableIRIn(); // Start the receiver
  
  armed = false;
}

void loop() {
  
  // put your main code here, to run repeatedly:
  
  if( armed ) {

    // Attiva allarme se PIR indica intruso
    int stato = digitalRead( PIR );
    digitalWrite( LED, stato );

    // Comunque vada, indica che l'antifurto è attivo
    digitalWrite( BOARD, HIGH );
  }
  else
  {
    // Se non è attivo, spegne tutte le uscite
    digitalWrite( BOARD, LOW );
    digitalWrite( LED, LOW );
  }
  
  // Riceve il comando dal telecomando
  if (irrecv.decode(&results)) {
    Serial.println(results.value, HEX);

    if( results.value == 0xA3C8EDDB ){
      Serial.println("OK!");
      armed = !armed;
    }
    
    irrecv.resume(); // Receive the next value
  }
  
  delay(100);
}
