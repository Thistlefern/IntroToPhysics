#include "baseGame.h"
#include "raylib/raylib.h"
#include "physObject.h"
#include <random>
#include "Shapes.h"
#include "enumUtil.h"
#include <iostream>

baseGame::baseGame()
{
	accumulatedFixedTime = 0.0f;
	targetFixedStep = 1.0f / 100.0f;

	// register what happens when a circle-circle pairing happens
	collMap[static_cast<collisionPair>(shapeType::CIRCLE | shapeType::CIRCLE)] = checkCircleCircle;
	// register what happens when an AABB-AABB pairing happens
	collMap[static_cast<collisionPair>(shapeType::AABB | shapeType::AABB)] = checkAABB2;
	// TODO do circle vs aabb
}

void baseGame::init()
{
	int screenWidth = 800;
	int screenHeight = 450;

	InitWindow(screenWidth, screenHeight, "raylib [core] example - basic window");

	SetTargetFPS(60);

	for (physObject &obj : physObjects)
	{
		obj.pos = glm::vec2(GetRandomValue(0.0f, screenWidth), GetRandomValue(0.0f, screenHeight));
	}

	onInit();
}

void baseGame::tick()
{
	accumulatedFixedTime += GetFrameTime();

	onTick();
}

void baseGame::tickFixed()
{
	accumulatedFixedTime -= targetFixedStep;

	// integrate and update positions
	for (size_t i = 0; i < physObjects.size(); i++)
	{
		physObjects[i].tickPhys(targetFixedStep);
	}

	// detect (and not yet resolve) collision
	for (size_t i = 0; i < physObjects.size(); i++)
	{
		for (size_t j = 0; j < physObjects.size(); j++)
		{
			// skip self collision
			if (i == j) { continue; }
			// skip collision on things that don't have a collider
			if (physObjects[i].collider.type == shapeType::NONE || physObjects[j].collider.type == shapeType::NONE) { continue; }

			int lhs = i;
			int rhs = j;

			// keep the lesser-ordered collider first
			if((uint8_t)physObjects[i].collider.type > (uint8_t)physObjects[j].collider.type)
			{
				// switch-a-roo
				lhs = j;
				rhs = i;
			}

			collisionPair pairing = (collisionPair)(physObjects[lhs].collider.type | physObjects[rhs].collider.type);
			bool collision = collMap[pairing](physObjects[lhs].pos, physObjects[lhs].collider, physObjects[rhs].pos, physObjects[rhs].collider);

			if (collision)
			{
				// Collision? Do Things
				std::cout << "Collision occurred at " << GetTime() << "!" << std::endl;
			}
		}
	}

	//TODO You can add force or whatevs here
	// object.addForce(glm::vec2(10.0f, 5.0f));
	// object.tickPhys(targetFixedStep);

	onTickFixed();
}

void baseGame::draw() const
{
	BeginDrawing();

	ClearBackground(RAYWHITE);
	
	for (physObject obj : physObjects)
	{
		obj.draw();
	}

	onDraw();

	EndDrawing();
}

void baseGame::exit()
{
	onExit();

	CloseWindow();
}

bool baseGame::shouldClose() const
{
	return WindowShouldClose();
}

bool baseGame::shouldTickFixed() const
{
	if (accumulatedFixedTime >= targetFixedStep)
	{
		return true;
	}
	else
	{
		return false;
	}
}