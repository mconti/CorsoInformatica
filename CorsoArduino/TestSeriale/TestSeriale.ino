/*
    TestSeriale
    posta@maurizioconti.com
    
    FabLab Romagna 2015 - Corso Arduino
 */
 
int liveCheck = 13;
int led = 4;
int servo = 6;
int potenziometro = 0;
int pulsante = 3;
int interruttore = 2;

// setup routine: Eseguita una volta sola, al reset
void setup() {                
  pinMode(led, OUTPUT);     
  pinMode(servo, OUTPUT);

  Serial.begin( 57600 );
  Serial.setTimeout(300);
  //Help();
}

//  loop routine: eseguita in continuazione
void loop() {

  // ------------------------------------- 
  // Live check - Ad ogni giro lampeggia...
  // (Indica che il circuito funziona)
  digitalWrite(liveCheck, HIGH);   
  delay(10);               
  digitalWrite(liveCheck, LOW);    
  delay(10);               

  // Se ci sono dati che arrivano dalla seriale...
  if ( Serial.available() > 0 ){

    // ...li legge formando una stringa completa. 
    String cmd = Serial.readString();

    // Verifica se la stringa arrivata è nel formato cmd.pin.valore!
    
    // Per farlo, individua le posizioni (indici) dove "tagliare" la stringa
    int start1 = 0;
    int end1 = cmd.indexOf('.');
    int start2 = end1 + 1;
    int end2 = cmd.indexOf('.', end1 + 1);
    int start3 = end2 + 1;
    int end3 = cmd.indexOf('!', end2 + 1);
  
    // Se non trova dei puntini o non trova il carattere !, indexOf torna -1
    // !!! Non va bene!
    if( (end1 < 0) || (end2 < 0) || (end3 < 0) )
    {
      // Diamo all'utente il messaggio di come si usa il programma.
      //Help();
    }
    else
    {
      // Se siamo qui, protrebbe essere arrivato un comando buono.
      
      // Ricava quindi le stringhe relative al comando (AR, DW etc), al pin e al valore  
      // Attenzione: "tagliando una String, otteniamo sempre delle string...
      String strCmd = cmd.substring( start1, end1 );      // qui dovrebbe esserci DW, AR, DR etc
      String strPin = cmd.substring( start2, end2 );      // qui dovrebbe esserci un numero
      String strValore = cmd.substring( start3, end3 );   // anche qui un numero.
    
      // ...quindi bisogna convertirle in numeri veri... ad esempio interi!
      int pin = strPin.toInt();
      int valore = strValore.toInt();
      
      // Seleziona l'azione giusta da fare guardando al comando
      if( strCmd == "AW" )
      {
        // Scrive in modo analogico sul pin (PWM)
        analogWrite( pin, valore );
    
        // Rilegge il pin e invia lo stato
        String valoreLetto = String(analogRead(pin));
        Serial.println( strPin + "=" + valoreLetto);
      }
      else if( strCmd == "AR" ) 
      {
        // Legge in modo analogico e invia lo stato
        String valoreLetto = String(analogRead(pin));
        Serial.println( strPin + "=" + valoreLetto);
      }
      else if( strCmd == "DW" ) 
      {
        // Scrive in modo digitale sul pin
        digitalWrite( pin, valore );
    
        // Rilegge il pin e invia lo stato
        String valoreLetto = String(digitalRead(pin));
        Serial.println( strPin + "=" + valoreLetto);
      }
      else if( strCmd == "DR" ) 
      {
        // Legge in modo digitale dal pin e invia lo stato
        String valoreLetto = String(digitalRead(pin));
        Serial.println( strPin + "=" + valoreLetto);
      }
      else 
      {
        // Invia alla seriale un messaggio che spiega come deve essere usato...
        //Help();
      }
    }
  }
}

void Help()
{
  Serial.println("-------------------------------------------------------------");
  Serial.println("posta@maurizioconti.com - FabLab Romagna 2015 - Corso Arduino");
  Serial.println("-------------------------------------------------------------");
  Serial.println("formato comandi: modo.pin.valore!");
  Serial.println("dove:");
  Serial.println("modo = AR, AW, DR, DW");
  Serial.println("pin = un qualsiasi pin valido (0,1,2,3 ... 12, 13 ...)");
  Serial.println("valore = un numero da 0 a N (0, 00, 23, 12344 ...)");
  Serial.println("");
  Serial.println("es:");
  Serial.println("AR.2!       (Legge valore analogico dal pin 2)");
  Serial.println("DW.13.0!    (Spegne il pin 13)");
  Serial.println("AW.10.128!  (Impone il 50% di duty-cycle al PWM del pin 10)");
  Serial.println("");
  Serial.println("Torna sempre il valore letto dal pin  nel formato pin=valore");
  Serial.println("");
}
