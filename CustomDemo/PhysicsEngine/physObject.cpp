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

// Given two physics objects, return the impuls to be applied
float resolveCollision(glm::vec2 posA, glm::vec2 velA, float massA,
					  glm::vec2 posB, glm::vec2 velB, float massB,
					  float elasticity, glm::vec2 normal)
{
	// caluclate the relative velocity
	glm::vec2 relVel = velA - velB;

	// calculate the magnitude of the impulse to apply
	float impulseMag = glm::dot(-(1.0f + elasticity) * relVel, normal) /
					   glm::dot(normal, normal * (1 / massA + 1 / massB));

	// return the impulse
	return impulseMag;
}

void resolvePhysBodies(physObject& lhs, physObject& rhs, float elasticity, const glm::vec2& normal, float pen)
{
	// calulate our resolution impulse
	float impulseMag = resolveCollision(lhs.pos, lhs.vel, lhs.mass,
										rhs.pos, rhs.vel, rhs.mass,
										elasticity, normal);

	glm::vec2 impulse = impulseMag * normal;

	// depenetrate the two objects
	pen *= 0.51f;

	// apply resolution impulses to both objects
	glm::vec2 correction = normal * pen;

	/*lhs.pos += correction;
	lhs.addImpulse(impulse);
	rhs.pos -= correction;
	rhs.addImpulse(-impulse);*/

	if (!lhs.isStatic)
	{
		lhs.pos += correction;
		lhs.addImpulse(impulse);
	}
	if (!rhs.isStatic)
	{
		rhs.pos -= correction;
		rhs.addImpulse(-impulse);
	}
}