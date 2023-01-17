import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

/**
 * Ход игры.
 */
public class Game {

    /**
     * Игровое поле.
     */
    char[][] field = new char[][]{
            {'.', '.', '.', '.', '.', '.', '.', '.'},
            {'.', '.', '.', '.', '.', '.', '.', '.'},
            {'.', '.', '.', '.', '.', '.', '.', '.'},
            {'.', '.', '.', '&', '*', '.', '.', '.'},
            {'.', '.', '.', '*', '&', '.', '.', '.'},
            {'.', '.', '.', '.', '.', '.', '.', '.'},
            {'.', '.', '.', '.', '.', '.', '.', '.'},
            {'.', '.', '.', '.', '.', '.', '.', '.'}};

    /**
     * Символ (на поле) игрока, который сейчас ходит.
     */
    char playerSymbol = '*';
    Scanner in = new Scanner(System.in);


    /**
     * Количество пропусков в текущей игре.
     */
    int countOfPasses = 0;

    /**
     * Размер игрового поля.
     */
    public int fieldSize = 8;

    /**
     * Создание игры.
     */
    public Game() {}

    /**
     * Показывает игровое поле.
     */
    public void showField() {
        for (int x = 0; x < fieldSize; x++) {
            StringBuilder line = new StringBuilder();
            for (int y = 0; y < fieldSize; y++) {
                line.append(field[x][y]).append('\t');
            }
            System.out.println(line);
        }
    }

    /**
     * Возвращает возможные ходы для текущего игрока в данный момент игры.
     * @return Возможные ходы.
     */
    public List<Move> showPossibleMoves() {
        List<Move> moves = new ArrayList<Move>();
        for (int x = 0; x < fieldSize; x++) {
            StringBuilder line = new StringBuilder();
            for (int y = 0; y < fieldSize; y++) {
                Move move = new Move(x, y, fieldSize);
                if (field[x][y] == '.' && isPossibleMove(move)) {
                    moves.add(move);
                    line.append(moves.size()).append("\t");
                }
                else {
                    line.append(field[x][y]).append("\t");
                }
            }
            System.out.println(line);
        }
        return moves;
    }

    /**
     * Ход игрока + выводит поле и возможные ходов.
     */
    public void playerMove() {
        System.out.println("Клетки, на которые можно сходить отмечены ненулевыми числами:");
        List<Move> moves = showPossibleMoves();
        if (moves.size() == 0) {
            countOfPasses += 1;
            System.out.println("Вы не можете никуда сходить, ход переходит сопернику.");
            playerSymbol = enemySymbol();
        }
        else {
            countOfPasses = 0;
            System.out.println("Введите номер клетки из возможных для хода " +
                    "(от 1 до " + moves.size() + ")");
            int moveId = Main.readInt();
            while (moveId < 1 || moveId > moves.size()) {
                System.out.println("Ошибка ввода! Введите номер клетки из возможных для хода " +
                        "(от 1 до " + moves.size() + ")");
                moveId = Main.readInt();
            }
            makeMove(moves.get(moveId - 1));
        }
    }

    /**
     * Вызывает метод, который заполняет клетки после хода (для всех окружаемых клеток)
     * @param move Совершаемый ход.
     */
    public void makeMove(Move move) {
        List<Move> endMoves = getEndMoves(move);
        for (Move endMove : endMoves) {
            fillAfterMove(move, endMove);
        }
        playerSymbol = enemySymbol();
    }

    /**
     * Сдвиг по координате Х для совершаемого хода и клетке,
     * вместе с которой будут окружены фишки противника.
     * @param move Совершаемный ход.
     * @param endMove Замыкающая клетка.
     * @return Сдвиг по Х.
     */
    public int xStep(Move move, Move endMove) {
        int xShift = endMove.getx() - move.getx();
        return Integer.compare(xShift, 0);
    }

    /**
     * Сдвиг по координате Y для совершаемого хода и клетке,
     * вместе с которой будут окружены фишки противника.
     * @param move Совершаемный ход.
     * @param endMove Замыкающая клетка.
     * @return Сдвиг по Y.
     */
    public int yStep(Move move, Move endMove) {
        int yShift = endMove.getY() - move.getY();
        return Integer.compare(yShift, 0);
    }

    /**
     * Заполнение клеток между ходом и замыкающей ход клеткой.
     * @param move Текущий ход.
     * @param endMove Замыкающая клетка.
     */
    public void fillAfterMove(Move move, Move endMove) {
        int xShift = xStep(move, endMove);
        int yShift = yStep(move, endMove);
        int x = move.getx();
        int y = move.getY();
        while (x != endMove.getx() || y != endMove.getY()) {
            field[x][y] = playerSymbol;
            x += xShift;
            y += yShift;
        }
    }

    /**
     * Возвращает символа, которым ходит противник.
     * @return Символ противника.
     */
    public char enemySymbol() {
        if (playerSymbol == '&') {
            return '*';
        }
        return '&';
    }

    /**
     * Выводит замыкающий ход, который можно совершить двигаясь в определенную сторону.
     * Если ход невозможен - возвращается ход с параметром с isPossible = false.
     * @param x1 X координата совершаемого хода.
     * @param y1 Y координата совершаемого хода.
     * @param xShift Направление хода по координате X.
     * @param yShift Направление хода по координате Y.
     * @param enemySymbol Символ. которым ходит противник.
     * @return Замыкающий ход для данного.
     */
    public Move getPossiblePosition(int x1, int y1, int xShift, int yShift, char enemySymbol) {
        Move move = new Move(8);
        int x = x1;
        int y = y1;
        while (x1 >= 0 && x1 < fieldSize && y1 >= 0 && y1 < fieldSize &&
                field[x1][y1] == enemySymbol) {
            x1 += xShift;
            y1 += yShift;
        }
        if (x1 < 0 || x1 >= fieldSize || y1 < 0 || y1 >= fieldSize || (x1 == x && y1 == y)) {
            return move;
        }
        if (field[x1][y1] == playerSymbol) {
            try {
                move.setX(x1);
                move.setY(y1);
            }
            catch (Exception e) {
                System.out.println(e.getMessage());
            }
        }
        return move;
    }

    /**
     * Получание замыкающих клеток для данного хода.
     * @param move Совершаемый ход.
     * @return Замыкающие ходы.
     */
    public List<Move> getEndMoves(Move move) {
        char enemySymbol = enemySymbol();
        List<Move> possibleMoves = new ArrayList<Move>();
        Move endMove = getPossiblePosition(move.getx() - 1, move.getY() - 1, -1, -1, enemySymbol);
        if (endMove.getPossibility()) {
            possibleMoves.add(endMove);
        }
        endMove = getPossiblePosition(move.getx() - 1, move.getY(), -1, 0, enemySymbol);
        if (endMove.getPossibility()) {
            possibleMoves.add(endMove);
        }
        endMove = getPossiblePosition(move.getx() - 1, move.getY() + 1, -1,  1, enemySymbol);
        if (endMove.getPossibility()) {
            possibleMoves.add(endMove);
        }
        endMove = getPossiblePosition(move.getx(), move.getY() - 1, 0, -1, enemySymbol);
        if (endMove.getPossibility()) {
            possibleMoves.add(endMove);
        }
        endMove = getPossiblePosition(move.getx(), move.getY() + 1, 0, 1, enemySymbol);
        if (endMove.getPossibility()) {
            possibleMoves.add(endMove);
        }
        endMove = getPossiblePosition(move.getx() + 1, move.getY() - 1, 1, -1, enemySymbol);
        if (endMove.getPossibility()) {
            possibleMoves.add(endMove);
        }
        endMove = getPossiblePosition(move.getx() + 1, move.getY(), 1, 0, enemySymbol);
        if (endMove.getPossibility()) {
            possibleMoves.add(endMove);
        }
        endMove = getPossiblePosition(move.getx() + 1, move.getY() + 1, 1, 1, enemySymbol);
        if (endMove.getPossibility()) {
            possibleMoves.add(endMove);
        }
        return possibleMoves;
    }

    /**
     * Проверка хода на возможность.
     * Если для данного хода нет ни одной замукающей клетки
     * (то есть той, на прямой с которой стоят символы противника, и на которой стоит символ текущегоо игрока).
     * @param move Совершаемый ход.
     * @return Возможен ли ход.
     */
    public boolean isPossibleMove(Move move) {
        List<Move> endMoves = getEndMoves(move);
        return endMoves.size() != 0;
    }

    /**
     * Проверка игры на возможность.
     * Игра возможна пока есть хотя бы одна свободная клетка и количество пропусков хода меньше 2.
     * @return Возможна ли игра.
     */
    public boolean gameIsPossible() {
        int countofEmpty = 0;
        for (int x = 0; x < fieldSize; x++) {
            for (int y = 0; y < fieldSize; y++) {
                if (field[x][y] == '.') {
                    countofEmpty++;
                }
            }
        }
        return countofEmpty > 0 && countOfPasses < 2;
    }

    /**
     * Возвращает количество очков для данного игрока.
     * @param playerSymbol Символ, которым ходит данный игрок.
     * @return Количество очков.
     */
    public int getResult(char playerSymbol) {
        int result = 0;
        for (int x = 0; x < fieldSize; x++) {
            for (int y = 0; y < fieldSize; y++) {
                if (field[x][y] == playerSymbol) {
                    result += 1;
                }
            }
        }
        return result;
    }
}

