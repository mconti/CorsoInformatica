void setup() {
  // put your setup code here, to run once:
  pinMode( 2, OUTPUT );
  pinMode( 3, INPUT );
  pinMode( 13, OUTPUT );
}

void loop() {
  // put your main code here, to run repeatedly:
  int stato = digitalRead( 3 );
  digitalWrite( 13, stato );
  digitalWrite( 2, stato );
}
