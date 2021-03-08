/*
Entrega de trabalho
Nós,

Jader Gedeon De Oliveira Rocha
Matheus Baltazar Ramos

declaramos que
todas as respostas são fruto de nosso próprio trabalho,
não copiamos respostas de colegas externos à equipe,
não disponibilizamos nossas respostas para colegas externos à equipe e
não realizamos quaisquer outras atividades desonestas para nos beneficiar
ou prejudicar outros.
*/

using System;
using System.Collections;
using System.Collections.Generic;

namespace Trabalho_1_EstruturaDeDados
{
    class Program
    {
        static void Main( string[] args )
        {

            string equacaoInfixa, equacaoPosfixa = "";

            Console.WriteLine( "Digite uma Equação Infixa: " );
            equacaoInfixa = Console.ReadLine();

            equacaoPosfixa = InfixaParaPosfixa( equacaoInfixa );

            Console.WriteLine( "\nA equação em modelo Posfixa fica: \n"+equacaoPosfixa+"\n" );
            Console.WriteLine( "E o resultado da equação é: " + PosfixaParaResultado( equacaoPosfixa ) );
          
        }

        private static String InfixaParaPosfixa( string infixa ) {

            // Dicionário que o algoritmo usa para verificar a prioridade dos itens da pilha
            var dicioPrioridades = new Dictionary<char, int>() {
                {'^', 1},
                {'*', 2},
                {'/', 2},
                {'%', 2},
                {'+', 3},
                {'-', 3},
                {'(', 4},
                {')', 4},
                };

            var pilhaOperandos = new Pilha<char>( infixa.Length );
            var posfixa = "";

            foreach ( char elemento in infixa ) {

                // Se o item for um dígito ele vai pra posfixa direto
                if ( Char.IsDigit( elemento ) ) {
                    posfixa += elemento;

                } else if ( dicioPrioridades[elemento] == 4 ) {

                    // Se o item for um ' ) ', ele retorna todos os valores até encontrar um ' ( ';
                    // Esses retornos são colocados na posfixa
                    if ( elemento == ')' ) {

                        while ( pilhaOperandos.peek() != '(' ) {

                            posfixa += pilhaOperandos.pop();

                        }

                        pilhaOperandos.pop();

                    } else {

                        pilhaOperandos.push( elemento );

                    }
                } else if ( dicioPrioridades[elemento] > 0 ) {

                    while ( !pilhaOperandos.isEmpty() && dicioPrioridades[pilhaOperandos.peek()] <= dicioPrioridades[elemento] ) {

                        posfixa += pilhaOperandos.pop();

                    }

                    pilhaOperandos.push( elemento );

                }
            }

            while ( !pilhaOperandos.isEmpty() ) {

                posfixa += ( pilhaOperandos.pop() );
            
            }

            return posfixa;
        }

        private static double PosfixaParaResultado( string posfixa ) {

            var pilhaOperadores = new Pilha<double>( posfixa.Length );

            foreach ( char elemento in posfixa ) {

                if ( Char.IsDigit( elemento ) ) {
                    pilhaOperadores.push( Char.GetNumericValue( elemento ) );

                } else {    

                    double ultimoNum = pilhaOperadores.pop();
                    double penultimoNum = pilhaOperadores.pop();
                    double result = 0;

                    switch ( elemento ) {

                        case '^':
                            result = Math.Pow( penultimoNum, ultimoNum );
                            break;
                        case '*':
                            result = penultimoNum * ultimoNum;
                            break;
                        case '/':
                            result = penultimoNum / ultimoNum;
                            break;
                        case '%':
                            result = penultimoNum % ultimoNum;
                            break;
                        case '+':
                            result = penultimoNum + ultimoNum;
                            break;
                        case '-':
                            result = penultimoNum - ultimoNum;
                            break;
                    }

                    pilhaOperadores.push( result );
                } 
            }    
            return pilhaOperadores.pop();
        }
    }
}

    class Pilha<Type>
    {
        // Um TAD Pilha teria os atributos com visibilidade interna (encapsulados). 
        private Type[] elementos;
        private int topo;
        // para inicializar os atributos teremos o contrutor
        public Pilha( int tam )
        {
            this.elementos = new Type[tam];
            this.topo = -1; // pilha esta vazia
        }
        public void push( Type elemento )
        {
            // se a pilha esta cheia gera uma excecao
            if ( this.isFull() )
                throw new Exception( "Pilha cheia." );

            this.topo++;
            elementos[topo] = elemento;
    }
        public Type pop()
        {
            // se a pilha estiver vazia gera uma excecao
            if ( this.isEmpty() )
                throw new Exception( "Pilha vazia." );

            Type itemDoTopo = elementos[topo];
            topo--;
            return itemDoTopo;
        }

        public Type peek()
        {
        // se a pilha estiver vazia gera uma excecao
        if ( this.isEmpty() )
            throw new Exception( "Pilha vazia." );

        return elementos[topo];
    }
        
        public bool isEmpty()
        {
            return topo == -1;
        }
        public bool isFull()
        {
            return topo >= elementos.Length - 1;
        }
    }