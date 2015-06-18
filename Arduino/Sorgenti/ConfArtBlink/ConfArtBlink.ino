
#include <IRremote.h>
#include <Adafruit_NeoPixel.h>

#define PIR 3
#define BUZZER 2
#define IR 4

#define PIXEL_PIN    5    // Digital IO pin connected to the NeoPixels.
#define PIXEL_COUNT 16

uint32_t nero = 0x00000000;
uint32_t rosso = 0x00FF0000;
uint32_t verde = 0x0000FF00;
uint32_t blu = 0x000000FF;

IRrecv telecomando(IR);
decode_results valoreLetto;
bool armed;
Adafruit_NeoPixel strip = Adafruit_NeoPixel(PIXEL_COUNT, PIXEL_PIN, NEO_GRB + NEO_KHZ800);


void setup() {
  // put your setup code here, to run once:
  pinMode( BUZZER , OUTPUT );
  pinMode( PIR , INPUT );
  
  Serial.begin( 9600 );
  //telecomando.enableIRIn(); // Start the receiver
  strip.begin();
  strip.show(); // Initialize all pixels to 'off'
    
  armed = true;
}

// put your main code here, to run repeatedly:
void loop() {
  
  //  il codice del tasto 1 è: 9716BE3F (2534850111 dec)
  //  il codice del tasto 0 è: C101E57B (3238126971 dec)
  
  /*if ( telecomando.decode(&valoreLetto) ) {
    Serial.println(valoreLetto.value, DEC);
    
    if ( valoreLetto.value == 2534850111 )
       armed = true;

    if ( valoreLetto.value == 3238126971 )
       armed = false;
    
    telecomando.resume(); // Receive the next value
  } */
    
  if ( armed ) {
    
    int stato = digitalRead( PIR );
  
    if( stato == 1 ) 
    {
      digitalWrite( BUZZER, HIGH );
      delay( 50 );

      strip.setPixelColor(0, rosso);   
      strip.setPixelColor(3, verde);   
      strip.setPixelColor(7, blu);   

      strip.show();  
  
      digitalWrite( BUZZER, LOW );
      delay( 50 );
    
    }
    else 
    {
      strip.setPixelColor(0, nero); 
      strip.setPixelColor(3, nero); 
      strip.setPixelColor(7, nero); 
      strip.show();  
    }
  }
  else
  {
    strip.setPixelColor(0, nero); 
    strip.show();  
  }
}
