#include "glm/glm.hpp"
#include "physObject.h"
#include "raylib/raylib.h"

physObject::physObject()
{
	// zero out pos and vel
	pos = vel = { 0, 0 };

	mass = 1.0f;
	totalForces = { 0,0 };
	gravity = false;
	gravPull = { 0, 1 };

	collider.type = shapeType::NONE;
}

void physObject::tickPhys(float delta)
{
	vel += totalForces * delta;
	totalForces = glm::vec2(0, 0);

	if(gravity)
	{
		addAcceleration(gravPull);
	}

	// euler integration
	// velocity into position
	pos += vel * delta;
}

void physObject::addForce(glm::vec2 force)
{
	totalForces += force / mass;
}

void physObject::addAcceleration(glm::vec2 force)
{
	totalForces += force;
}

void physObject::addImpulse(glm::vec2 force)
{
	vel += force / mass;
}

void physObject::addVelocity(glm::vec2 force)
{
	vel += force;
}

void physObject::draw() const
{
	switch (collider.type)
	{
		case shapeType::NONE:
			DrawPixel(pos.x, pos.y, RED);
			break;
		case shapeType::CIRCLE:
			DrawCircle(pos.x, pos.y, collider.circleData.radius, BLUE);
			break;
		case shapeType::AABB:
			DrawRectangle(pos.x, pos.y, collider.aabbData.length, collider.aabbData.length, GREEN);
			break;
		case shapeType::SQUARE:
			DrawRectangle(pos.x, pos.y, 15, 15, YELLOW);
		default:
			break;
	}
}