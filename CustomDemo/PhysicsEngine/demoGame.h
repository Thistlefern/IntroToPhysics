#pragma once
#include "baseGame.h"
#include "physObject.h"

class demoGame : public baseGame
{
	void onDraw() const override;
	void onTick() override;
};