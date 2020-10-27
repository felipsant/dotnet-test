# Backend test

Welcome to Docplanner Group technical test

### Instructions

- **Please take some time to read the whole readme page.**
- This exercise is meant to be done in pair / mob programming with the candidate, discussing and coding the solution step by step. Discussions, proposed solutions, and proper implementation strategy will be taken into account.
- Take some time as well to get acquainted with the code before proceeding with any modification.
- This code is imperfect on purpose, you have full freedom in modifying anything in the solution and creating new files, folders and projects as well. We recommend to keep the in memory database with the current setup so you don't have to deal with setting up another database provider.

### Context

Welcome to Docplanner automated hospital. Ironically, by trying to make the healthcare more human we created a hospital which 
is fully ran by robots. In this project there is a small solution we use to allow us to check for available robots when a patient comes to the hospital
with a specific condition so we can treat them.

### Goal

The current solution has an endpoint `/robots/available` which returns one robot that can treat a specific condition. However, we want to modify our system:
- Extend the `robots/available` endpoint so it can take a DateTime object as a parameter, and return only the robots not busy during that time
- Implement the `required_rooms` endpoint, which should return the maximum number of robots that will have an appointment at the same time during the given date. 
  - b) Information returned by this endpoint does not change often, we want this information to be cached. Could you use the IMemoryCache implementation to achieve it?
- The system is currently notifying multiple departments when one of the robots is selected in this endpoint, we want to **add a notification** to marketing department as well so they can post a picture on social media.

Needless to say, we'd like to have a **better and clearer code** after you complete any modification :D

### Attribution

© All rights reserved to Doctoralia Internet S.L.
