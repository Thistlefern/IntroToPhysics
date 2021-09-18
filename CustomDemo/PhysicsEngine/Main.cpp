#include "baseGame.h"
#include "demoGame.h"
#include "physObject.h"

int main()
{
	baseGame* game = new demoGame();
	game->init();
	physObject object;

	while(!game->shouldClose())
	{
		game->tick();
		game->draw();

		while (game->shouldTickFixed())
		{
			game->tickFixed();
		}
	}

	game->exit();

	delete game;

	return 0;
}