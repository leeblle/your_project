using System;
using System.IO;
using System.Text;


namespace binary_tree_c_sharp
{
    public class Tree
    {
        // Класс дерево
        public class Node
        {
            // подкласс Узел, т.е. элемент дерева
            public int value; // значение узла
            public Node left; // левый узел
            public Node right; // правый узел

            // конструктор для узла
            // используется для инициализации корня дерева
            public Node(int val)
            {
                this.value = val;
            }
        }
        //public Node root;
        public Node TreeNode; // экземпляр класса Node

        //нерекурсивное добавление
        public void AddNoRecursive(int val) //узел и его значение
        {
            Node parent = null; // ссылаемся на родителя текущей вершины
            Node node = TreeNode; // ссылаемся на текущую вершину

            // идем по дереву и ищем место для вставки
            // когда переменная node примет нулевое значение, место для вставки найдено
            while (node != null)
            {
                parent = node;
                // если меньше, вставляем слева
                if (val < node.value)
                {
                    node = node.left;
                }
                // если больше, справа
                else if (val > node.value)
                {
                    node = node.right;
                }
                // если равен, то значение уже есть в дереве и вставлять не нужно
                else
                {
                    return;
                }
            }
            // создаём новую вершину
            Node newNode = new Node(val);

            // в предке parent исправляем ссылку left или right в зависимости от того, влево или вправо мы пошли из parent
            if (parent == null) // если изначально было пусто, то нужно обновить корень
            {
                TreeNode = newNode;
            }
            else if (val < parent.value)
            {
                parent.left = newNode;
            }
            else if (val > parent.value)
            {
                parent.right = newNode;
            }

        }

        // Рекурсивное добавление значения в дерево
        public void Add(ref Node node, int val)
        {
            // передаем значение узла по ссылке (через ref), чтобы передвигаться по дереву без возвращения значений
            // т.е. после каждого добавления значения мы знаем на каком узле находимся, так как ходим по ссылкам

            // Если это первое значение, то инициализируем корень дерева
            if (node == null)
            {
                node = new Node(val);
            }
            // если добавляемое значение меньше чем значение в текущем узле дерева, то добавляем в левое поддерево
            if (val < node.value)
            {
                Add(ref node.left, val);
            }
            // если добавляемое значение больше чем значение в текущем узле дерева, то добавляем в правое поддерево
            else if (val > node.value)
            {
                Add(ref node.right, val);
            }
        }

        // прямой обход
        // двигаемся от корня сначала влево, потом вправо
        // сначала рекурсивно проходим всю левую часть, потом также правую
        public void DirecTraversal(Node node, ref string s)
        {
            // в строку s добавляем значение при проходе по дереву и накапливаем результат в ссылке (через ref), чтобы потом можно
            // было бы вытащить оттуда значение
            if (node != null)
            {
                // запомнинаем текущее значение
                s += node.value.ToString() + " ";
                // обходим рекурсивно левое поддерево
                DirecTraversal(node.left, ref s);
                // обходим рекурсивно правое поддерево
                DirecTraversal(node.right, ref s);
            }
        }


        // обратный обход
        // двигаемся от левой части к центру, потом в правое поддерево
        public void ReverseTraversal(Node node, ref string s)
        {
            if (node != null)
            {
                // обходим рекурсивно левое поддерево
                ReverseTraversal(node.left, ref s);
                // запомнинаем текущее значение
                s += node.value.ToString() + " ";
                // обходим рекурсивно правое поддерево
                ReverseTraversal(node.right, ref s);
            }
        }

        // концевой обход
        // двигаемся от правой части к центру, потом в левое поддерево
        public void EndTraversal(Node node, ref string s)
        {
            /*
            Аргументы метода:
            1. TreeNode node - текущий "элемент дерева" (ref  передача по ссылке)       
            2. ref string s - строка, в которой накапливается результат (ref - передача по ссылке)
            */
            if (node != null)
            {
                // обходим рекурсивно правое поддерево
                EndTraversal(node.right, ref s);
                // запомнинаем текущее значение
                s += node.value.ToString() + " ";
                // обходим рекурсивно левое поддерево
                EndTraversal(node.left, ref s);
            }
        }


    }

    class Program
    {

        // метод сохранения текста в файл
        public static void WriteToFile(string text, string fileName)
        {
            // создаем или открываем (если уже создан) файл в директории, где выполняется код
            string path = AppDomain.CurrentDomain.BaseDirectory + fileName;
            Console.WriteLine(String.Format("Результат сохранен в файл сохранен в: {0}", path));
            // записываем весь, накопленный текст
            File.WriteAllText(path, text);
        }


        static void TestRecursive()
        {
            // Создаем экземпляр дерева
            Tree tree = new Tree();
            // Добавляем в него значения
            tree.Add(ref tree.TreeNode, 12);
            tree.Add(ref tree.TreeNode, 2);
            tree.Add(ref tree.TreeNode, 6);
            tree.Add(ref tree.TreeNode, 8);
            tree.Add(ref tree.TreeNode, 4);
            tree.Add(ref tree.TreeNode, 7);
            tree.Add(ref tree.TreeNode, 11);
            tree.Add(ref tree.TreeNode, 10);
            tree.Add(ref tree.TreeNode, 17);
            tree.Add(ref tree.TreeNode, 5);

            string s = "Прямой обход: ";
            tree.DirecTraversal(tree.TreeNode, ref s);

            s += "\nОбратный обход: ";
            tree.ReverseTraversal(tree.TreeNode, ref s);

            s += "\nКонцевой обход: ";
            tree.EndTraversal(tree.TreeNode, ref s);

            // Записываем результат в файл
            WriteToFile(s, "recursive_result.txt");
            Console.WriteLine(s);
        }

        static void TestNoRecursive()
        {
            // Создаем экземпляр дерева
            Tree tree = new Tree();
            // Добавляем в него значения
            tree.AddNoRecursive(12);
            tree.AddNoRecursive(2);
            tree.AddNoRecursive(6);
            tree.AddNoRecursive(8);
            tree.AddNoRecursive(4);
            tree.AddNoRecursive(7);
            tree.AddNoRecursive(11);
            tree.AddNoRecursive(10);
            tree.AddNoRecursive(17);
            tree.AddNoRecursive(5);


            string s = "Прямой обход: ";
            tree.DirecTraversal(tree.TreeNode, ref s);

            s += "\nОбратный обход: ";
            tree.ReverseTraversal(tree.TreeNode, ref s);

            s += "\nКонцевой обход: ";
            tree.EndTraversal(tree.TreeNode, ref s);

            // Записываем результат в файл
            WriteToFile(s, "no_recursive_result.txt");
            Console.WriteLine(s);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("=============Рекурсивная логика=============");
            TestRecursive();

            Console.WriteLine();

            Console.WriteLine("=============Нерекурсивная логика=============");
            TestNoRecursive();
        }
    }
}