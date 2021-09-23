#include "demoGame.h"
#include "raylib/raylib.h"
#include "physObject.h"
#include <iostream>

void demoGame::onDraw() const
{
	ClearBackground(RAYWHITE);
}

void demoGame::onTick()
{
	// FPS Counter
	char fps[5];
	_itoa_s(GetFPS(), fps, 10);
	SetWindowTitle(fps);

	// Spawning logic
	if(IsMouseButtonPressed(0))
	{
		physObject newObject;

		Vector2 cursorPos = GetMousePosition();
		newObject.pos.x = cursorPos.x;
		newObject.pos.y = cursorPos.y;

		newObject.vel.x = GetRandomValue(-30, 30);
		newObject.vel.y = GetRandomValue(-30, 30);

		newObject.collider.type = shapeType::CIRCLE;
		newObject.collider.circleData.radius = (int)GetRandomValue(10, 50);

		newObject.isStatic = false;

		physObjects.push_back(newObject);
	}
	if (IsMouseButtonPressed(1))
	{
		physObject newObject;
		Vector2 cursorPos = GetMousePosition();
		newObject.pos.x = cursorPos.x;
		newObject.pos.y = cursorPos.y;

		newObject.vel.x = GetRandomValue(-50, 50);
		newObject.vel.y = GetRandomValue(-50, 50);

		newObject.collider.type = shapeType::AABB;
		newObject.collider.aabbData.length = (int)GetRandomValue(5, 50);

		newObject.isStatic = false;

		physObjects.push_back(newObject);
	}

	if (IsMouseButtonPressed(2))
	{
		std::cout << "Middle" << std::endl;

		physObject newObject;
		Vector2 cursorPos = GetMousePosition();
		newObject.pos.x = cursorPos.x;
		newObject.pos.y = cursorPos.y;

		newObject.vel.x = 0.0f;
		newObject.vel.y = 0.0f;

		newObject.collider.type = shapeType::CIRCLE;
		newObject.collider.circleData.radius = (int)GetRandomValue(20, 40);

		newObject.isStatic = true;

		physObjects.push_back(newObject);
	}
}