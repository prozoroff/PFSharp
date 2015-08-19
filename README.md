# PFSharp
C# implementation of particle filter

  Particle filter is a modeling method for estimating the state of the system that cannot be fully observed. Particle filter keeps the weighted normalized set of sample states S={s1, s2, ..., sm}, called particles. 

  At each step, after getting the measurement o (or a vector of measurements), particle filter performs the following actions:
 
1. Creating new sample m of system model states from X state
2. Jump to a new state in the Markov model of the robot position: P (X"| X'). This action simulates the motion of the robot in the space
3. Weighting of each state of Markov model according to observations
4. Normalization of weights for a new set of states

  Particle filter is well suited for solving the problem of localization, where we need to track the position of the object, which is a hidden value. Jump between the states is the movement of the object, and observation is the result of motion measurements. Both of these values are very noisy. Motion model for the different cases can be quite different, but they are all will take into account the systematic and random errors one way or another.
