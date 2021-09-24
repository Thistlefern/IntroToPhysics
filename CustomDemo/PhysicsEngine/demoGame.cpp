#include "demoGame.h"
#include "raylib/raylib.h"
#include "physObject.h"
#include <iostream>
#include <sstream>

bool worldGrav = false;
int worldGravPull = 40;

void demoGame::onDraw() const
{
	ClearBackground(RAYWHITE);
	DrawText("Left click to spawn dynamic circle", 5, 5, 15, BLUE);
	DrawText("Right click to spawn static circle", 5, 20, 15, ORANGE);
	DrawText("Center click to turn gravity on/off", 5, 35, 15, BLACK);
	DrawText("Use up/down arrows to adjust the strength of gravity", 710, 5, 15, BLACK);
	
	if(worldGrav)
	{
		DrawText("Gravity: On", 710, 20, 15, BLACK);
	}
	else
	{
		DrawText("Gravity: Off", 710, 20, 15, BLACK);
	}

	std::stringstream stream;
	stream << worldGravPull;
	DrawText(stream.str().c_str(), 710, 35, 15, BLACK);
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

		newObject.vel.x = GetRandomValue(-50, 50);
		newObject.vel.y = GetRandomValue(-50, 50);

		newObject.collider.type = shapeType::CIRCLE;
		newObject.collider.circleData.radius = (int)GetRandomValue(10, 50);

		newObject.isStatic = false;
		newObject.gravity = worldGrav;
		newObject.gravPull.y = worldGravPull;

		physObjects.push_back(newObject);
	}
	if (IsMouseButtonPressed(1))
	{
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

		/*physObject newObject;
		Vector2 cursorPos = GetMousePosition();
		newObject.pos.x = cursorPos.x;
		newObject.pos.y = cursorPos.y;

		newObject.vel.x = GetRandomValue(-50, 50);
		newObject.vel.y = GetRandomValue(-50, 50);

		newObject.collider.type = shapeType::AABB;
		newObject.collider.aabbData.length = (int)GetRandomValue(5, 50);

		newObject.collider.aabbData.min.x = newObject.pos.x;
		newObject.collider.aabbData.min.y = newObject.pos.y + newObject.collider.aabbData.length;
		newObject.collider.aabbData.max.x = newObject.pos.x + newObject.collider.aabbData.length;
		newObject.collider.aabbData.max.y = newObject.pos.y;


		newObject.isStatic = false;

		physObjects.push_back(newObject);*/
	}

	if (IsMouseButtonPressed(2))
	{
		worldGrav = !worldGrav;

		for (size_t i = 0; i < physObjects.size(); i++)
		{
			physObjects[i].gravity = worldGrav;
			physObjects[i].gravPull.y = worldGravPull;
		}
	}

	if(IsKeyPressed(KEY_UP))
	{
		worldGravPull += 20;
		for (size_t i = 0; i < physObjects.size(); i++)
		{
			physObjects[i].gravity = worldGrav;
			physObjects[i].gravPull.y = worldGravPull;
		}
	}
	if(IsKeyPressed(KEY_DOWN))
	{
		worldGravPull -= 20;
		for (size_t i = 0; i < physObjects.size(); i++)
		{
			physObjects[i].gravity = worldGrav;
			physObjects[i].gravPull.y = worldGravPull;
		}
	}
}