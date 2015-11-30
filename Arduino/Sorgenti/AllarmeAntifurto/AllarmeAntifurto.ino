
// Maurizio Conti - FabLab Romagna 2015
// posta@maurizioconti.com

int contatore;

void setup() {
  pinMode( 2, OUTPUT );
  Serial.begin( 115200 );
  
  contatore = 100; 
}

void loop() {

  int luce = analogRead( A0 );
  if ( luce < 400 )
  {
     contatore = 100; 
     Serial.println("Luce spenta");
  }
  else
  {
    // La luce è accesa...
    Serial.println("Luce accesa");

    contatore-- ;
    if (contatore  <= 0 )
    {
       Serial.println("Beeep!");
       
       digitalWrite( 2, HIGH );
       delay( 500 );
       digitalWrite( 2, LOW );
    }
  }

  delay( 100 );
}
