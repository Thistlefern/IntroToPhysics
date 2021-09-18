#pragma once
#include "glm/glm.hpp"
#include "Shapes.h"

class physObject
{
    // Sum of all continuous forces being applied
    glm::vec2 totalForces;
public:
    // The position of the object
    glm::vec2 pos;
    // The velocity of the object
    glm::vec2 vel;

    // Should gravity be applied?
    bool gravity;
    // What is the gravitational pull?
    glm::vec2 gravPull;

    float mass;

    shape collider;

    physObject();

    void tickPhys(float delta);

    // Apply continuous force that considers mass
    void addForce(glm::vec2 force);
    // Apply continuous force, regardless of mass
    void addAcceleration(glm::vec2 force);
    // Apply instantaneous force that considers mass
    void addImpulse(glm::vec2 force);
    // Apply instantaneous force, regardless of mass
    void addVelocity(glm::vec2 force);

    void draw() const;
};