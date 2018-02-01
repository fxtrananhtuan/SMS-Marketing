Short Summary
There are many different kinds of applications SMS applications in the market today, and many others are being developed. Applications in which SMS messaging can be utilized are virtually unlimited. Some common examples of these are given below:

- Person-to-person text messaging is the most commonly used SMS application, and it is what the SMS technology was originally designed for.
Many content providers make use of SMS text messages to send information such as news, weather report, and financial data to their subscribers.
- SMS messages can carry binary data, and so SMS can be used as the transport medium of wireless downloads. Objects such as ringtones, wallpapers, pictures, and operator logos can be encoded in SMS messages.
- SMS is a very suitable technology for delivering alerts and notifications of important events.
SMS messaging can be used as a marketing tool.

In general, there are two ways to send SMS messages from a computer / PC to a mobile phone:
- Connect a mobile phone or GSM/GPRS modem to a computer / PC. Then use the computer / PC and AT commands to instruct the mobile phone or GSM/GPRS modem to send SMS messages.
- Connect the computer / PC to the SMS center (SMSC) or SMS gateway of a wireless carrier or SMS service provider. Then send SMS messages using a protocol / interface supported by the SMSC or SMS gateway.
In this article, I will explain the first way to send, read, and delete SMS using AT commands.
In order to send a SMS, you need an aWavecom GMS any moden in this Application I using Model M1306B. 
Wavecome Allow to connect with any program language by sending AT command.

AT commands are instructions used to control a modem. AT is the abbreviation of ATtention. Every command line starts with "AT" or "at". That's why modem commands are called AT commands. There are two types of AT commands:

Basic commands are AT commands that do not start with a "+". For example, D (Dial), A (Answer), H (Hook control), and O (Return to online data state) are the basic commands.
Extended commands are AT commands that start with a "+". All GSM AT commands are extended commands. For example, +CMGS (Send SMS message), +CMGL (List SMS messages), and +CMGR (Read SMS messages) are extended commands.

This example using Three Layer Architecture.

![3lyrs2](https://user-images.githubusercontent.com/14042198/35670305-4cf5f266-078c-11e8-8580-2b35767b4d7f.jpg)



