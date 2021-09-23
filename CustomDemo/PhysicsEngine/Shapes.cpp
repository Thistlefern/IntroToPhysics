#include "Shapes.h"
#include <iostream>

bool checkCircleCircle(glm::vec2 posA, circle circleA, glm::vec2 posB, circle circleB)
{
	glm::vec2 offset = posA - posB;
	float distance = glm::length(offset);
	float radiiSum = circleA.radius + circleB.radius;

	return distance < radiiSum;	// if(distance < radiiSum){return true} else {false}
}

bool checkCircleCircle(const glm::vec2& posA, const shape& shapeA, const glm::vec2& posB, const shape& shapeB)
{
	return checkCircleCircle(posA, shapeA.circleData, posB, shapeB.circleData);
}

// ----- Here begins my sorry attempt to understand -----
bool checkAABB2(glm::vec2 posA, aabb aabbA, glm::vec2 posB, aabb aabbB)
{
	aabbA.min = { posA.x - aabbA.length, posA.y };
	aabbA.max = { posA.x, posA.y + aabbA.length };
	
	aabbB.min = { posB.x - aabbB.length, posB.y };
	aabbB.max = { posB.x, posB.y + aabbB.length };

	return aabbA.min.x < aabbB.max.x
		&& aabbA.max.x > aabbB.min.x
		&& aabbA.min.y < aabbB.max.y
		&& aabbA.max.y > aabbB.min.y;

		/*&& aabbA.min.z < aabbB.max.z
		&& aabbA.max.z > aabbB.min.z;*/
}

bool checkAABB2(const glm::vec2& posA, const shape& shapeA, const glm::vec2& posB, const shape& shapeB)
{
	return checkAABB2(posA, shapeA.aabbData, posB, shapeB.aabbData);
}

//bool checkCircleAABB(glm::vec2 posA, circle circle, glm::vec2 posB, aabb aabb)
//{
//	// if the distance between the two positions is less than the sum of the radius and the apothem, they are colliding
//}

// bool checkCircleAABB(glm::vec2& posA, const shape& shapeA, const glm::vec2& posB, const shape& shapeB);

glm::vec2 depenetrateCircleCircle(const glm::vec2& posA, const shape& shapeA, const glm::vec2& posB, const shape& shapeB, float& pen)
{
	// offset between the two objects
	glm::vec2 offset = posA - posB;
	// sum of the radii
	float radiiSum = shapeA.circleData.radius + shapeB.circleData.radius;
	float dist = glm::length(offset);

	// write the penetration depth
	pen = radiiSum - dist;

	// return the collision normal
	return glm::normalize(offset);

}