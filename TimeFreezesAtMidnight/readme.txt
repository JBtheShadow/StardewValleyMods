================================================================================
Time Freezes at Midnight
By JBtheShadow
Version 1.1.1
================================================================================
To alter the moment when the game stops advancing the time just change the value of the "timeFreezesAt" field.
It should be a number representing military time between 700 (7AM) and 2540 (1:40AM), inclusive, in increments of 10.
The default value is 2400 a.k.a. midnight/12:00AM

Here's a little hourly table of in game values and how the game displays them:

Time displayed | Value
      7AM      | 0700
      8AM      | 0800
      9AM      | 0900
     10AM      | 1000
     11AM      | 1100
     12PM      | 1200
      1PM      | 1300
      2PM      | 1400
      3PM      | 1500
      4PM      | 1600
      5PM      | 1700
      6PM      | 1800
      7PM      | 1900
      8PM      | 2000
      9PM      | 2100
     10PM      | 2200
     11PM      | 2300
     12AM      | 2400
      1AM      | 2500
      2AM      | 2600

Notice that times past midnight are still counted as part of the same day so instead of looping back to zero, like most clocks, in here they keep counting until 2AM.
Is only then when you head to bed, or your character collapses, that time is reset back to 7AM.