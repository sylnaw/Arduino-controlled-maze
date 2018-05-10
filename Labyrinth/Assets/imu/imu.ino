#include<Wire.h>
#define MPU9255_ADDRESS 0x68

int16_t AcX, AcY, AcZ, GyX, GyY, GyZ;

void setup()
{
      Wire.begin();
      Wire.beginTransmission(MPU9255_ADDRESS);
      Wire.write(0x6B);  // PWR_MGMT_1 register
      Wire.write(0);     // set to zero (wakes up the MPU-6050)
      Wire.endTransmission(true); 
      Serial.begin(115200);
}

void loop()
{
      int16_t rawDataAcc[6];
      Wire.beginTransmission(MPU9255_ADDRESS);
      Wire.write(0x3B);
      Wire.endTransmission(false);
      int i = 0;
      Wire.requestFrom(MPU9255_ADDRESS, 6);

      Serial.print('!');

      while (Wire.available()>0) 
      {
      rawDataAcc[i++] = Wire.read();
      }  
      
      AcX = rawDataAcc[0] << 8 | rawDataAcc[1] ;
      AcY = rawDataAcc[2] << 8 | rawDataAcc[3] ;
      AcZ = rawDataAcc[4] << 8 | rawDataAcc[5] ;
    
      Serial.print(AcX);
      Serial.print(",");
      Serial.print(AcY);
      Serial.print(",");
      Serial.print(AcZ);
      Serial.print(";");

      uint8_t rawDataGy[6];
      Wire.beginTransmission(MPU9255_ADDRESS);
      Wire.write(0x43);
      Wire.endTransmission(false);
      i = 0;
      Wire.requestFrom(MPU9255_ADDRESS, 6);

      while (Wire.available()) 
      {
      rawDataGy[i++] = Wire.read();
      }  
      
      GyX = rawDataGy[0] << 8 | rawDataGy[1] ;
      GyY = rawDataGy[2] << 8 | rawDataGy[3] ;
      GyZ = rawDataGy[4] << 8 | rawDataGy[5] ;

      Serial.print(GyX);
      Serial.print(",");
      Serial.print(GyY);
      Serial.print(",");
      Serial.print(GyZ);
      Serial.println("");

      Serial.flush();

      delay(50);
}


