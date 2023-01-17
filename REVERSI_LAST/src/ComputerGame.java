import java.util.ArrayList;
import java.util.List;

/**
 * Игра с компьютером.
 */
public class ComputerGame extends Game {

    /**
     * Игрок.
     */
    Player player;

    /**
     * Создание игры с компьютером и игроком.
     * @param player Игрок.
     */
    public ComputerGame(Player player) {
        this.player = player;
    }

    /**
     * Запуск игры,
     * игры продолжается пока пропуском меньше 2х и не все клетки заняты.
     */
    public void startGameProcess() {
        while (gameIsPossible()) {
            showField();
            if (playerSymbol == '*') {
                System.out.println(player.name + ", Ваш ход!");
                playerMove();
            }
            else {
                System.out.println("Сейчас ходит компьютер.");
                computerMove();
            }
        }
        showResult();
    }

    /**
     * Получение ss значения для формулы расчета следующего хода.
     * @param move Совершаемый ход.
     * @return Ss значение.
     */
    public double ssValue(Move move) {
        if ((move.getx() == 0 || move.getx() == fieldSize - 1) &&
                (move.getY() == 0 || move.getY() == fieldSize - 1)) {
            return 0.8;
        }
        if ((move.getx() == 0 || move.getx() == fieldSize - 1) ||
                (move.getY() == 0 || move.getY() == fieldSize - 1)) {
            return 0.4;
        }
        return 0;
    }

    /**
     * Получение si значения для формулы расчета следующего хода.
     * @param move клетка, которая будет окружена.
     * @return Si значение.
     */
    public int siValue(Move move) {
        if ((move.getx() == 0 || move.getx() == fieldSize - 1) ||
                (move.getY() == 0 || move.getY() == fieldSize - 1)) {
            return 2;
        }
        return 1;
    }

    /**
     * Возвращает количество очков, которые будут получены при данном ходе.
     * @param move Ход, для которого получается количество очков.
     * @return Количество очков.
     */
    public double getCountOfPoint(Move move) {
        double countOfPOints = ssValue(move);
        List<Move> endMoves = getEndMoves(move);
        for (Move endMove : endMoves) {
            int xShift = xStep(move, endMove);
            int yShift = yStep(move, endMove);
            int x = move.getx();
            int y = move.getY();
            while (x < endMove.getx() || y < endMove.getY()) {
                x += xShift;
                y += yShift;
                countOfPOints += siValue(new Move(x, y, 8));
            }
        }
        return countOfPOints;
    }

    /**
     * Выбор индекса хода, при котором количество получаемых очков - максимально.
     * @param points Максимальное количество очков.
     * @return Индекс хода (в массиве).
     */
    public int maxPointsValue(List<Double> points) {
        double max = -1;
        int maxIndex = 0;
        for (int i = 0; i < points.size(); i++) {
            if (points.get(i) > max) {
                max = points.get(i);
                maxIndex = i;
            }
        }
        return maxIndex;
    }

    /**
     * Расчет хода для компьютера.
     */
    public void computerMove() {
        List<Move> moves = new ArrayList<Move>();
        List<Double> points = new ArrayList<Double>();
        for (int x = 0; x < fieldSize;  x++) {
            for (int y = 0; y < fieldSize; y++) {
                if (field[x][y] == '.') {
                    Move move = new Move(x, y, 8);
                    if (isPossibleMove(move)) {
                        moves.add(move);
                        points.add(getCountOfPoint(move));
                    }
                }
            }
        }
        if (moves.size() == 0) {
            System.out.println("Компьютер не может никуда сходить! Ход переходит вам.");
            countOfPasses++;
        }
        else {
            makeMove(moves.get(maxPointsValue(points)));
        }
    }

    /**
     * Вывод результата игры.
     */
    public void showResult() {
        int result = getResult('*');
        player.setBestResult(result);
        int computerResult = getResult('&');
        if (result > computerResult) {
            System.out.println("Вы победили! \n Со счетом: " + result + ":" + computerResult);
        }
        else {
            System.out.println("Вы проиграли(\n Со счетом: " + result + ":" + computerResult);
        }
    }
}

