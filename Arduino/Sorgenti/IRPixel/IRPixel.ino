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
#define NUMPIXEL 3

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
        pixels.setPixelColor(0, pixels.Color(0,0,0)); // Moderately bright green color.
        pixels.setPixelColor(1, pixels.Color(0,0,0)); // Moderately bright green color.
        pixels.setPixelColor(2, pixels.Color(0,0,0)); // Moderately bright green color.
        pixels.show(); // This sends the updated pixel color to the hardware.
    }

    if( results.value == 0xFFA25D ) {
        pixels.setPixelColor(0, pixels.Color(0,150,0)); // Moderately bright green color.
        pixels.setPixelColor(1, pixels.Color(0,150,0)); // Moderately bright green color.
        pixels.setPixelColor(2, pixels.Color(0,150,0)); // Moderately bright green color.
        pixels.show(); // This sends the updated pixel color to the hardware.
    }

    if( results.value == 0xFF629D ) {
        pixels.setPixelColor(0, pixels.Color(150,0,0)); // Moderately bright green color.
        pixels.setPixelColor(1, pixels.Color(150,0,0)); // Moderately bright green color.
        pixels.setPixelColor(2, pixels.Color(150,0,0)); // Moderately bright green color.
        pixels.show(); // This sends the updated pixel color to the hardware.
    }

    if( results.value == 0xFFE21D ) {
        pixels.setPixelColor(0, pixels.Color(0,0,150)); // Moderately bright green color.
        pixels.setPixelColor(1, pixels.Color(0,0,150)); // Moderately bright green color.
        pixels.setPixelColor(2, pixels.Color(0,0,150)); // Moderately bright green color.
        pixels.show(); // This sends the updated pixel color to the hardware.
    }
  }
  
  delay(100);
}
