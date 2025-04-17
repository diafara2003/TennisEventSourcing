# Tenis List test

## condiciones
Tennis has a rather quirky scoring system, and to newcomers it can be a little difficult to keep track of. 💥
The tennis society has contracted you to build a scoreboard to display the current score during tennis games.


A game is won by the first player to have won at least four points in total and at least two points more than the opponent.
The running score of each game is described in a manner peculiar to tennis: scores from zero to three points are described as “Love”, “Fifteen”, “Thirty”, and “Forty” respectively.
If at least three points have been scored by each player, and the scores are equal, the score is “Deuce”.
If at least three points have been scored by each side and a player has one more point than his opponent, the score of the game is “Advantage” for the player in the lead.
You need only report the score for the current game. Sets and Matches are out of scope.

💥==>red
✅==>green
♻️==>refactor

# List
[ ] - Debo mostrar en el marcador Love cuando se inicie el marcador
[ ] - Debo mostrar en el marcador Fifteen cuando el jugador 1 gane un punto
	Se cambio por
		Debo sumar en el marcador un punto al jugador 1 y el marcador debe mostrar Fifteen-0
[ ] - Dedo poder sumar al marcador un punto cuando el jugador 1 gane el punto
	se Agrego theory y se valido los casos hasta 40

[ ] - Debo mostrar en el marcador 15 cuando el jugador 2 gane un punto
[ ] - Debo mostrar en el marcador 40 cuando el jugador 1 gane dos puntos
[ ] - Debo mostrar en el marcador Deuce cuando ambos jugadores tengan tres puntos
[ ] - Debo mostrar en el marcador Advantage cuando un jugador tenga cuatro puntos y el otro tres
	en el refactor lo pase a theory
[ ] - Debo mostrar en el marcador Game cuando el jugador 1 tenga cuatro puntos y dos más que su oponente

