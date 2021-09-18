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
		newObject.collider.circleData.radius = (int)GetRandomValue(5, 50);

		physObjects.push_back(newObject);
	}
	if (IsMouseButtonPressed(1))
	{
		physObject newObject;
		Vector2 cursorPos = GetMousePosition();
		newObject.pos.x = cursorPos.x;
		newObject.pos.y = cursorPos.y;

		newObject.vel.x = GetRandomValue(-30, 30);
		newObject.vel.y = GetRandomValue(-30, 30);

		newObject.collider.type = shapeType::AABB;
		newObject.collider.aabbData.length = (int)GetRandomValue(5, 50);

		physObjects.push_back(newObject);
	}
}