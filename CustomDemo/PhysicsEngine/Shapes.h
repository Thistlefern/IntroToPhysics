#pragma once
#include "glm/glm.hpp"

struct circle
{
	float radius;
};

struct aabb
{
	float length;
	glm::vec2 min;
	glm::vec2 max;
};

enum class shapeType : uint8_t
{
	NONE = 0,			// 0000 0000
	CIRCLE = 1 << 0,	// 0000 0001
	AABB = 1 << 1,		// 0000 0010
	SQUARE = 1 << 2,	// 0000 0100
};

struct shape
{
	shapeType type;

	union
	{
		circle circleData;
		aabb aabbData;
	};
};

bool checkCircleCircle(glm::vec2 posA, circle circleA, glm::vec2 posB, circle circleB);

bool checkCircleCircle(const glm::vec2& posA, const shape& shapeA, const glm::vec2& posB, const shape& shapeB);

// ----- Here begins my sorry attempt to understand -----
bool checkAABB2(glm::vec2 posA, aabb aabbA, glm::vec2 posB, aabb aabbB);

bool checkAABB2(const glm::vec2& posA, const shape& shapeA, const glm::vec2& posB, const shape& shapeB);

bool checkCircleAABB(glm::vec2 posA, circle circle, glm::vec2 posB, aabb aabb);

bool checkCircleAABB(glm::vec2& posA, const shape& shapeA, const glm::vec2& posB, const shape& shapeB);

glm::vec2 depenetrateCircleCircle(const glm::vec2& posA,
								  const shape& shapeA,
								  const glm::vec2& posB,
								  const shape& shapeB,
								  float& pen);
