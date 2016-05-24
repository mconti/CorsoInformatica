/*
 * Riceve da IR un codice
 * Accende un NeoPixel
 * posta@maurizioconti.com
 * 
 * Basata sulla LIB di Ken Shirriff
 * http://arcfn.com
 */

#include <IRremote.h>

#include <Adafruit_NeoPixel.h>
#ifdef __AVR__
  #include <avr/power.h>
#endif

#define RX_PIN 3
decode_results results;

#define LED_PIN 7
#define NUMPIXELS 3

IRrecv irrecv( RX_PIN );
Adafruit_NeoPixel pixels = Adafruit_NeoPixel(NUMPIXELS, LED_PIN, NEO_GRB + NEO_KHZ800);

void setup()
{
  Serial.begin(9600);
  irrecv.enableIRIn(); // Start the receiver
  pixels.begin(); // This initializes the NeoPixel library
}

void loop() {
  
  if (irrecv.decode(&results)) {
    Serial.println(results.value, HEX);
    irrecv.resume(); // Receive the next value

    if( results.value == 0xFF22DD ) {
        pixels.setPixelColor(0, pixels.Color(0,0,0)); // Spegne tutto
        pixels.setPixelColor(1, pixels.Color(0,0,0));  
        pixels.setPixelColor(2, pixels.Color(0,0,0)); 
        pixels.show(); // This sends the updated pixel color to the hardware.
    }

    if( results.value == 0xFF52AD ) {
        //                                   R   G   B
        pixels.setPixelColor(0, pixels.Color(250,200,000)); // il più vicino ai fili rosso
        pixels.setPixelColor(1, pixels.Color(250,200,000));  
        pixels.setPixelColor(2, pixels.Color(250,200,000)); 
        pixels.show(); // This sends the updated pixel color to the hardware.
    }

    if( results.value == 0xFFA25D ) {
        pixels.setPixelColor(0, pixels.Color(0,150,0)); // Moderately bright green color.
        pixels.setPixelColor(1, pixels.Color(0,150,0)); // Moderately bright green color.
        pixels.setPixelColor(2, pixels.Color(0,150,0)); // Moderately bright green color.
        pixels.show(); // This sends the updated pixel color to the hardware.
    }

    if( results.value == 0xFF629D ) {
        pixels.setPixelColor(0, pixels.Color(250,0,0)); // bright red color.
        pixels.setPixelColor(1, pixels.Color(250,0,0)); 
        pixels.setPixelColor(2, pixels.Color(250,0,0)); 
        pixels.show(); // This sends the updated pixel color to the hardware.
    }

    delay(1000);
  }

  int valore = analogRead( A2 );
  Serial.println( valore );
  
      pixels.setPixelColor(0, pixels.Color(0,0,0)); 
    pixels.setPixelColor(1, pixels.Color(0,0,0)); 
    pixels.setPixelColor(2, pixels.Color(0,0,0)); 
    pixels.show(); // This sends the updated pixel color to the hardware.
    delay(valore);

    pixels.setPixelColor(0, pixels.Color(250,0,0)); 
    pixels.show(); // This sends the updated pixel color to the hardware.
    delay(valore);

    pixels.setPixelColor(1, pixels.Color(0,0,250)); 
    pixels.show(); // This sends the updated pixel color to the hardware.
    delay(valore);

    pixels.setPixelColor(2, pixels.Color(0,250,0)); 
    pixels.show(); // This sends the updated pixel color to the hardware.
    delay(valore);

}
