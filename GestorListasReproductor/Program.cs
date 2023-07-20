﻿using GestorListasReproductor.Models;
using System.Text.RegularExpressions;

namespace GestorListasReproductor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LogConsole("\nEjecutando tareas para obtener el listado de rreproduccion!\n");
            var ListaReproduccion = CreateListaReproduccion();
            LogConsole("Ingresaste : " + ListaReproduccion.Count()+ " Videos \n\n");
            var ListReproduccionOrder = ListaReproduccion.OrderBy(x => x.Duracion).ToList();
            List<Video> autores = new List<Video>();
            ListReproduccionOrder.ForEach(x =>
            { 
                if (!autores.Where(a=> a.Autor.Contains(x.Autor)).Any())
                {
                    x.Cantidad = ListReproduccionOrder.Where(a => a.Autor == x.Autor).Count();
                    autores.Add(x);
                }
                    
            });
            var test = autores.OrderByDescending(x => x.Cantidad).ToList();
            test.ForEach(autor =>LogConsole($"El autor {autor.Autor} tiene {ListReproduccionOrder.Where(x => x.Autor == autor.Autor ).Count()}"));
            Console.ReadKey();
            LogConsole("----------------------------------------------------------------------");
            GetObtenerVideosMasCortos(ListReproduccionOrder);
        }
        #region Metodos Privados
        private static void GetObtenerVideosMasCortos(List<Video> listReproduccionOrder)
        {
            var videosOrdenadosDuracion = listReproduccionOrder.OrderBy(x => x.Duracion).ToList();
            
            videosOrdenadosDuracion.ForEach(x =>LogConsole($"{GetTime(x.Duracion)}.{x.Titulo}"));
        }

        private static string GetTime(int totalSeconds)
        {
            int hor, min, seg;
            hor = totalSeconds / 3600;
            min = ((totalSeconds - hor * 3600) / 60);
            seg = totalSeconds - (hor * 3600 + min * 60);
            return $"\n{hor}:{min}:{seg}\n";
        }

        /// <summary>
        /// Obtiene un listado de videos, de la lista de reporduccion enviada.
        /// </summary>
        /// <returns></returns>
        private static List<Video> CreateListaReproduccion()
        {
            string videos = $"2:49:25\r\nREPRODUCIENDO\r\nEl amor | Por Darío Sztajnszrajber\r\nFacultad Libre\r\n•\r\n1,8 M de visualizaciones • hace 6 años\r\n\r\n\r\n1:09:10\r\nREPRODUCIENDO\r\nCrea paso a paso una app con React.js y Redux\r\nEDteam\r\n•\r\n77 K visualizaciones • hace 4 años\r\n\r\n\r\n10:39\r\nREPRODUCIENDO\r\nHow to THINK in English | No More Translating in Your Head!\r\nRachel's English\r\n•\r\n10 M de visualizaciones • hace 3 años\r\n\r\n\r\n54:53\r\nREPRODUCIENDO\r\nDe código 💩 a código limpio 🦄 - Refactoring tips ✨ | la función CodelyTV 22\r\nCodelyTV - Redescubre la programación\r\n•\r\n50 K visualizaciones • Emitido hace 3 años\r\n\r\n\r\n43:47\r\nREPRODUCIENDO\r\nFilosofía para tu desarrollo personal - TU MEJOR VERSIÓN Cómo obtener mejores resultados - Jim Rohn\r\nGrandes Tesoros vive\r\n•\r\n2,3 M de visualizaciones • hace 2 años\r\n\r\n\r\n18:57\r\nREPRODUCIENDO\r\n¿Cómo armar un primer viaje a Europa?\r\nSir Chandler\r\n•\r\n121 K visualizaciones • hace 3 años\r\n\r\n\r\n47:03\r\nREPRODUCIENDO\r\n¿Te Cuesta ELEVAR TU VIBRACIÓN? Haz Esto YA!\r\nLAIN - La Voz De Tu Alma\r\n•\r\n2,6 M de visualizaciones • hace 2 años\r\n\r\n\r\n11:26\r\nREPRODUCIENDO\r\nProgramación Imperativa VS Programación Declarativa\r\nhdeleon.net\r\n•\r\n10 K visualizaciones • hace 2 años\r\n\r\n\r\n32:59\r\nREPRODUCIENDO\r\nEl Secreto METAFÍSICO de la MANIFESTACIÓN de tu realidad\r\nLAIN - La Voz De Tu Alma\r\n•\r\n368 K visualizaciones • hace 2 años\r\n\r\n\r\n13:59\r\nREPRODUCIENDO\r\n💃 useRef y useImperativeHandle. CONTROLA tus COMPONENTES de forma IMPERATIVA ☝️ (Bootcamp FullStack)\r\nmidudev\r\n•\r\n16 K visualizaciones • hace 2 años\r\n\r\n\r\n1:51:05\r\nREPRODUCIENDO\r\nWebinar de Selección de Perfiles IT\r\nEducaciónBIZ\r\n•\r\n1,3 K visualizaciones • Emitido hace 2 años\r\n\r\n\r\n40:03\r\nREPRODUCIENDO\r\nComo me hice millonario paso a paso! | Salomondrin\r\nAlejandro Salomon - Emprendeduro\r\n•\r\n1,7 M de visualizaciones • hace 2 años\r\n\r\n\r\n11:01\r\nREPRODUCIENDO\r\nMortal Kombat, La Nueva Adaptación Del 2021 | #TeLoResumo\r\nTe lo resumo\r\n•\r\n4,8 M de visualizaciones • hace 2 años\r\n\r\n\r\n11:08\r\nREPRODUCIENDO\r\nTareas en Segundo Plano en ASP.Net | Hosted Services\r\nhdeleon.net\r\n•\r\n20 K visualizaciones • hace 2 años\r\n\r\n\r\n23:47\r\nREPRODUCIENDO\r\nAplicación Web en Tiempo Real ejecutada en Segundo Plano en ASP .Net | Hosted Services y SignalR\r\nhdeleon.net\r\n•\r\n16 K visualizaciones • hace 2 años\r\n\r\n\r\n16:34\r\nREPRODUCIENDO\r\nAplicaciones en Tiempo Real utilizando Blazor y SignalR\r\nhdeleon.net\r\n•\r\n13 K visualizaciones • hace 2 años\r\n\r\n\r\n15:05\r\nREPRODUCIENDO\r\nArquitectura frontend y productividad - Entrevista con Jason Lengstorf (JavaScript)\r\nmidudev\r\n•\r\n19 K visualizaciones • hace 2 años\r\n\r\n\r\n7:33\r\nREPRODUCIENDO\r\n👀 MIRA ESTO ANTES DE ELEGIR TU HOSTING WORDPRESS\r\nCarlos Azaustre - Aprende JavaScript\r\n•\r\n2,2 K visualizaciones • hace 2 años\r\n\r\n\r\n15:08\r\nREPRODUCIENDO\r\n🔑 Crear Primary Keys sin Conexión, El uso del Identificador GUID\r\nhdeleon.net\r\n•\r\n4,9 K visualizaciones • hace 2 años\r\n\r\n\r\n14:20\r\nREPRODUCIENDO\r\n⚠️ Este ERROR Me Hizo Perder MUCHO DINERO ⚠️\r\nEuge Oller\r\n•\r\n92 K visualizaciones • hace 2 años\r\n\r\n\r\n20:19\r\nREPRODUCIENDO\r\n7 AÑOS DESDE MI DESPERTAR ESPIRITUAL: esto es lo que he aprendido | Pilar Sousa\r\nPilar Sousa\r\n•\r\n29 K visualizaciones • hace 2 años\r\n\r\n\r\n15:23\r\nREPRODUCIENDO\r\n📚 Los MEJORES LIBROS de AUTOAYUDA y SUPERACIÓN PERSONAL de Todos los Tiempos [GRATIS]\r\nVíctor Martín\r\n•\r\n121 K visualizaciones • hace 2 años\r\n\r\n\r\n8:26\r\nREPRODUCIENDO\r\n5 ALGORITMOS que DEBERÍAS (al menos) conocer\r\nBettaTech\r\n•\r\n209 K visualizaciones • hace 2 años\r\n\r\n\r\n13:40\r\nREPRODUCIENDO\r\n¿Cómo Programar en C# .Net?\r\nhdeleon.net\r\n•\r\n22 K visualizaciones • hace 2 años\r\n\r\n\r\n14:34\r\nREPRODUCIENDO\r\nNET MAUI - Te cuento todo lo que debes sabes\r\nMauro Bernal\r\n•\r\n5,1 K visualizaciones • hace 2 años\r\n\r\n\r\n5:38\r\nREPRODUCIENDO\r\n¿Cómo trabajar las consultas con Join en las bases de datos?\r\nhdeleon.net\r\n•\r\n14 K visualizaciones • hace 2 años\r\n\r\n\r\n13:46\r\nREPRODUCIENDO\r\nMAKING RATAOUILLE From Pixar's Ratatouille (Confit Byaldi) | Lets Recreate Ep. 1\r\nTwo Plaid Aprons\r\n•\r\n7,2 M de visualizaciones • hace 2 años\r\n\r\n\r\n16:08\r\nREPRODUCIENDO\r\n\U0001f92f CREA tu propio Router en React SIN dependencias - FullStack Bootcamp JavaScript\r\nmidudev\r\n•\r\n8 K visualizaciones • hace 2 años\r\n\r\n\r\n11:37\r\nREPRODUCIENDO\r\nManejar colecciones de Tareas Asíncronas en .Net\r\nhdeleon.net\r\n•\r\n4,5 K visualizaciones • hace 2 años\r\n\r\n\r\n14:20\r\nREPRODUCIENDO\r\nGetting started with API Load Testing (Stress, Spike, Load, Soak)\r\nNick Chapsas\r\n•\r\n151 K visualizaciones • hace 2 años\r\n\r\n\r\n16:18\r\nREPRODUCIENDO\r\n¿Qué es MVC?\r\nhdeleon.net\r\n•\r\n24 K visualizaciones • hace 2 años\r\n\r\n\r\n12:14\r\nREPRODUCIENDO\r\nCódigo más Limpio con el Patrón MVC y Service Layer\r\nhdeleon.net\r\n•\r\n13 K visualizaciones • hace 2 años\r\n\r\n\r\n9:07\r\nREPRODUCIENDO\r\n¿Cómo utilizar localStorage en Javascript? \U0001f95e\U0001f95e\r\nhdeleon.net\r\n•\r\n21 K visualizaciones • hace 1 año\r\n\r\n\r\n1:39:41\r\nREPRODUCIENDO\r\n17 Hábitos para Vivir con Abundancia que te Permitirán Crear una Mentalidad de Riqueza y Abundancia\r\nSECRETOS DE LA VIDA\r\n•\r\n292 K visualizaciones • hace 1 año\r\n\r\n\r\n11:12\r\nREPRODUCIENDO\r\nLos 4 principios para construir DISCIPLINA según Marco Aurelio y Séneca | Estoicismo paso a paso\r\nJorge Benito\r\n•\r\n2,8 M de visualizaciones • hace 1 año\r\n\r\n\r\n18:15\r\nREPRODUCIENDO\r\n¿Vale la pena Programar en Java?\r\nhdeleon.net\r\n•\r\n72 K visualizaciones • hace 1 año\r\n\r\n\r\n23:23\r\nREPRODUCIENDO\r\nCómo estructurar un proyecto en React de manera escalable - Tomás Bustamante\r\nnerdearla\r\n•\r\n1,6 K visualizaciones • hace 1 año\r\n\r\n\r\n25:16\r\nREPRODUCIENDO\r\n#Infra para #devs Todo lo que tenes que saber - Juani Gallo\r\nnerdearla\r\n•\r\n855 visualizaciones • hace 1 año\r\n\r\n\r\n26:04\r\nREPRODUCIENDO\r\n[2023 ]☑ ¿Cuál es el mejor camino para la programación?\r\nnerdearla\r\n•\r\n34 K visualizaciones • hace 1 año\r\n\r\n\r\n21:57\r\nREPRODUCIENDO\r\nAvanzando hacia una #arquitectura basada en #microservicios - Jesús Marín\r\nnerdearla\r\n•\r\n1,2 K visualizaciones • hace 1 año\r\n\r\n\r\n9:58\r\nREPRODUCIENDO\r\nClaves para una relación amorosa y libre | Máster en Pareja y Sexualidad Consciente | Kuestiona\r\nBorja Vilaseca\r\n•\r\n36 K visualizaciones • hace 1 año\r\n\r\n\r\n21:11\r\nREPRODUCIENDO\r\n5 Cosas que son DIFÍCILES de Entender cuando se Comienza a Programar (Explicadas)\r\nhdeleon.net\r\n•\r\n21 K visualizaciones • hace 1 año\r\n\r\n\r\n15:55\r\nREPRODUCIENDO\r\n\U0001f92f Estrategias para Hablar y Entender a Extraños\r\nEuge Oller\r\n•\r\n55 K visualizaciones • hace 1 año\r\n\r\n\r\n6:27\r\nREPRODUCIENDO\r\nCOMPOSITE (Así Es Como Funciona Unity) | PATRONES de DISEÑO\r\nBettaTech\r\n•\r\n33 K visualizaciones • hace 1 año\r\n\r\n\r\n6:00\r\nREPRODUCIENDO\r\n¿Te ha parado por la calle algún motivad@? | Striptease emocional | Borja Vilaseca\r\nBorja Vilaseca\r\n•\r\n14 K visualizaciones • hace 1 año\r\n\r\n\r\n12:44\r\nREPRODUCIENDO\r\nLo que he Aprendido tras una Depresión | Euge Oller\r\nEuge Oller\r\n•\r\n90 K visualizaciones • hace 1 año\r\n\r\n\r\n16:59\r\nREPRODUCIENDO\r\nAsí automaticé una tarea de 2 horas con Javascript.\r\nFalconMasters\r\n•\r\n165 K visualizaciones • hace 1 año\r\n\r\n\r\n1:03:10\r\nREPRODUCIENDO\r\nREACCIONAMOS a tu CURRÍCULUM Parte 2 (Frontend, Backend y Fullstack) | #laFunción 7x01\r\nCodelyTV - Redescubre la programación\r\n•\r\n15 K visualizaciones • Emitido hace 1 año\r\n\r\n\r\n9:45\r\nREPRODUCIENDO\r\nEl pan turco sin horno es el pan más delicioso y fácil que jamás haya preparado. Suave y esponjoso\r\nLina'nın Yemek Tarifleri\r\n•\r\n44 M de visualizaciones • hace 1 año\r\n\r\n\r\n16:08\r\nREPRODUCIENDO\r\nCarne de Exportación vs Carne Local ¿Lo mejor se va para afuera? | Locos X el Asado\r\nLocos X el Asado\r\n•\r\n385 K visualizaciones • hace 1 año\r\n\r\n\r\n11:03\r\nREPRODUCIENDO\r\nLA MEJOR PIZZA DE MADRID 🍕 | XOKAS EATS #7\r\nelxokas\r\n•\r\n1,7 M de visualizaciones • hace 1 año\r\n\r\n\r\n20:08\r\nREPRODUCIENDO\r\n¿Cuales Son Las Peores Peliculas De Dinosaurios? | #TeLoResumo\r\nTe lo resumo\r\n•\r\n3,1 M de visualizaciones • hace 1 año\r\n\r\n\r\n16:47\r\nREPRODUCIENDO\r\nCreación de Contenido HTML Dinámicamente (3 formas distintas)\r\nhdeleon.net\r\n•\r\n13 K visualizaciones • hace 1 año\r\n\r\n\r\n28:31\r\nREPRODUCIENDO\r\n\U0001f9c5 ¿Qué es la ARQUITECTURA CEBOLLA? | Onion Architecture\r\nManuel Zapata\r\n•\r\n13 K visualizaciones • hace 1 año\r\n\r\n\r\n22:06\r\nREPRODUCIENDO\r\nOPTIMIZA tu Programación en FRONTEND con MEMOIZATION | JAVASCRIPT ↗️\r\nhdeleon.net\r\n•\r\n10 K visualizaciones • hace 1 año\r\n\r\n\r\n8:22\r\nREPRODUCIENDO\r\nInteligencia emocional, puede fallar | Paula Irueste | TEDxUniversidadNacionaldeCórdoba\r\nTEDx Talks\r\n•\r\n12 K visualizaciones • hace 1 año\r\n\r\n\r\n28:28\r\nREPRODUCIENDO\r\nLa MEJOR MANERA de Aprender FLEXBOX con el Juego de la RANA DE CSS 🐸\r\nhdeleon.net\r\n•\r\n7,1 K visualizaciones • hace 1 año\r\n\r\n\r\n23:04\r\nREPRODUCIENDO\r\nMap vs Object en JavaScript. ¿Qué son los Map y cuándo usarlos? 🤔\r\nmidulive\r\n•\r\n63 K visualizaciones • hace 1 año\r\n\r\n\r\n15:34\r\nREPRODUCIENDO\r\nNo Podés Volver a Hacer Chimichurri Sin Ver Este Capítulo | Receta de Locos X el Asado\r\nLocos X el Asado\r\n•\r\n1 M de visualizaciones • hace 1 año\r\n\r\n\r\n8:02\r\nREPRODUCIENDO\r\nOvejas Asesinas | #TeLoResumo\r\nTe lo resumo\r\n•\r\n1,4 M de visualizaciones • hace 1 año\r\n\r\n\r\n16:27\r\nREPRODUCIENDO\r\n.NET y DOCKER! - Cómo desplegar tus aplicaciones// Containers, .NET, Dockerfiles y MUCHO MÁS!\r\nThe Coder Cave esp\r\n•\r\n22 K visualizaciones • hace 1 año\r\n\r\n\r\n13:45\r\nREPRODUCIENDO\r\n¿Qué llevo en mi bolso de viaje tech?\r\nSupraPixel\r\n•\r\n38 K visualizaciones • hace 1 año\r\n\r\n\r\n25:46\r\nREPRODUCIENDO\r\nEvolución de la Gestión de estado en React\r\nCodelyTV - Redescubre la programación\r\n•\r\n13 K visualizaciones • hace 1 año\r\n\r\n\r\n1:22:04\r\nREPRODUCIENDO\r\nPerdí a un amigo\r\nHolaMundo\r\n•\r\n29 K visualizaciones • Emitido hace 1 año\r\n\r\n\r\n8:16\r\nREPRODUCIENDO\r\nComo AFRONTAR las ENTREVISTAS en INGLÉS (Cómo Ingeniero de Software)\r\nBettaTech\r\n•\r\n13 K visualizaciones • hace 1 año\r\n\r\n\r\n10:55\r\nREPRODUCIENDO\r\n🌎 URLPattern, la nueva API para extraer información de rutas en JavaScript\r\nmidulive\r\n•\r\n16 K visualizaciones • hace 1 año\r\n\r\n\r\n5:50\r\nREPRODUCIENDO\r\nCAMBIOS FÍSICOS en el GYM | RECOPILACIÓN | Motivación para seguir adelante\r\nTheYohaN\r\n•\r\n53 K visualizaciones • hace 1 año\r\n\r\n\r\n18:01\r\nREPRODUCIENDO\r\nLeo DiCaprio ¿Es Realmente El Mejor Actor?\r\nTe lo resumo\r\n•\r\n4,9 M de visualizaciones • hace 1 año\r\n\r\n\r\n16:17\r\nREPRODUCIENDO\r\n¿Cuánto COBRAR por una APLICACIÓN?\r\nhdeleon.net\r\n•\r\n52 K visualizaciones • hace 1 año\r\n\r\n\r\n18:07\r\nREPRODUCIENDO\r\n\U0001f7e8 Pipe en JAVASCRIPT | Programación Funcional\r\nhdeleon.net\r\n•\r\n20 K visualizaciones • hace 1 año\r\n\r\n\r\n8:58\r\nREPRODUCIENDO\r\n7 ERRORES al Estimar un PROYECTO ⛔\r\nhdeleon.net\r\n•\r\n4,9 K visualizaciones • hace 1 año\r\n\r\n\r\n1:11:18\r\nREPRODUCIENDO\r\nCURSO de ARRAYS en JavaScript (Básico-Intermedio)\r\nhdeleon.net\r\n•\r\n49 K visualizaciones • hace 1 año\r\n\r\n\r\n10:25\r\nREPRODUCIENDO\r\nTodo Sobre el Vacío Tradicional Argentino | Receta de Locos X el Asado\r\nLocos X el Asado\r\n•\r\n687 K visualizaciones • hace 1 año\r\n\r\n\r\n26:59\r\nREPRODUCIENDO\r\nGit Merge vs Rebase vs Squash ¿Qué estrategia debemos elegir?\r\nCodelyTV - Redescubre la programación\r\n•\r\n40 K visualizaciones • hace 1 año\r\n\r\n\r\n18:19\r\nREPRODUCIENDO\r\n5 COSAS que me Hubiera Gustado SABER cuando comencé a PROGRAMAR en TypeScript\r\nhdeleon.net\r\n•\r\n11 K visualizaciones • hace 1 año\r\n\r\n\r\n36:45\r\nREPRODUCIENDO\r\nProgramación FUNCIONAL en TypeScript GRATIS\r\nhdeleon.net\r\n•\r\n12 K visualizaciones • hace 1 año\r\n\r\n\r\n26:38\r\nREPRODUCIENDO\r\nAnalizando el Plan de Estudios de Ingeniería en Computación\r\nhdeleon.net\r\n•\r\n7,1 K visualizaciones • hace 1 año\r\n\r\n\r\n14:24\r\nREPRODUCIENDO\r\nThe Last Kingdom ⚔️ TEMPORADA 5 ⚔️ RESUMEN EXPLICADO ❌El Destino llego a su FIN ...\r\nTOP XSeriesX\r\n•\r\n116 K visualizaciones • hace 1 año\r\n\r\n\r\n1:42:08\r\nREPRODUCIENDO\r\nEn VIVO en nuevo estudio!: programación en NZ #11\r\nHolaMundo\r\n•\r\n26 K visualizaciones • Emitido hace 1 año\r\n\r\n\r\n16:59\r\nREPRODUCIENDO\r\n⛏ CÓMO MINAR tu propia BLOCKCHAIN con JAVASCRIPT\r\nCarlos Azaustre - Aprende JavaScript\r\n•\r\n6,5 K visualizaciones • hace 1 año\r\n\r\n\r\n31:01\r\nREPRODUCIENDO\r\nBasic Authentication en BACKEND 🔒\r\nhdeleon.net\r\n•\r\n35 K visualizaciones • hace 1 año\r\n\r\n\r\n28:19\r\nREPRODUCIENDO\r\nPROGRAMANDO VIDA ARTIFICIAL con JavaScript, Programando Autómatas Celulares \U0001f9a0\r\nhdeleon.net\r\n•\r\n33 K visualizaciones • hace 1 año\r\n\r\n\r\n16:24\r\nREPRODUCIENDO\r\nResuelvo ENTREVISTA TÉCNICA Programador JR utilizando solo PROGRAMACIÓN FUNCIONAL\r\nhdeleon.net\r\n•\r\n17 K visualizaciones • hace 1 año\r\n\r\n\r\n20:01\r\nREPRODUCIENDO\r\n¿Qué Es El Efecto Keanu Reeves? | #TeLoResumo\r\nTe lo resumo\r\n•\r\n3,2 M de visualizaciones • hace 1 año\r\n\r\n\r\n18:50\r\nREPRODUCIENDO\r\n5 PROBLEMAS SOLUCIONADOS con REGEX\r\nhdeleon.net\r\n•\r\n16 K visualizaciones • hace 1 año\r\n\r\n\r\n11:05\r\nREPRODUCIENDO\r\n5 TIPS con WHERE en SQL\r\nhdeleon.net\r\n•\r\n19 K visualizaciones • hace 1 año\r\n\r\n\r\n10:00\r\nREPRODUCIENDO\r\nEstrenando mi Nuevo Asador con Picañas | La Capital\r\nLa Capital\r\n•\r\n3 M de visualizaciones • hace 1 año\r\n\r\n\r\n35:12\r\nREPRODUCIENDO\r\nÈPICO VLOG DOCUMENTAL || DESEMBARCO EN NORMANDÍA\r\nMomo\r\n•\r\n399 K visualizaciones • hace 1 año\r\n\r\n\r\n10:45\r\nREPRODUCIENDO\r\nPastrami | La Capital\r\nLa Capital\r\n•\r\n3,3 M de visualizaciones • hace 1 año\r\n\r\n\r\n15:54\r\nREPRODUCIENDO\r\n5 COSAS EXTRAÑAS en JavaScript\r\nhdeleon.net\r\n•\r\n10 K visualizaciones • hace 1 año\r\n\r\n\r\n9:36\r\nREPRODUCIENDO\r\nLo que la meditación puede hacer por tu cerebro | Nazareth Castellanos | TEDxTarragona\r\nTEDx Talks\r\n•\r\n253 K visualizaciones • hace 1 año\r\n\r\n\r\n6:49\r\nREPRODUCIENDO\r\n¿Que hacer si no podes comprar dolares?\r\nAgustin Romeo\r\n•\r\n12 K visualizaciones • hace 1 año\r\n\r\n\r\n47:39\r\nREPRODUCIENDO\r\nAPI REST con NODE.js || GUÍA de BUENAS PRÁCTICAS\r\nCarlos Azaustre - Aprende JavaScript\r\n•\r\n53 K visualizaciones • hace 11 meses\r\n\r\n\r\n19:19\r\nREPRODUCIENDO\r\nRobert De Niro, El Ascenso Y La Caida | #TeLoResumo\r\nTe lo resumo\r\n•\r\n2,6 M de visualizaciones • hace 11 meses\r\n\r\n\r\n9:44\r\nREPRODUCIENDO\r\nLos 5 MEJORES Patrones de Diseño de Software\r\nCodelyTV - Redescubre la programación\r\n•\r\n15 K visualizaciones • hace 11 meses\r\n\r\n\r\n1:30:16\r\nREPRODUCIENDO\r\nTirando Bola temp 6 ep 22. - Rivers y Roberto Martínez\r\nFranco Escamilla\r\n•\r\n4,2 M de visualizaciones • hace 10 meses\r\n\r\n\r\n13:49\r\nREPRODUCIENDO\r\nRECUPERA TUS EMAILS BORRADOS CON ESTE SOFT!\r\nTechnoReviews\r\n•\r\n6,5 K visualizaciones • hace 10 meses\r\n\r\n\r\n14:39\r\nREPRODUCIENDO\r\nMI SETUP GAMER Y UNIVERSITARIO | V3 2022\r\nAtomicDragonoy\r\n•\r\n217 K visualizaciones • hace 9 meses\r\n\r\n\r\n16:46\r\nREPRODUCIENDO\r\nComiendo en el restaurante MÁS FAMOSO del MUNDO - SALT BAE NUEVA YORK\r\nIbai\r\n•\r\n5 M de visualizaciones • hace 9 meses\r\n\r\n\r\n22:29\r\nREPRODUCIENDO\r\n10 Atributos Poco Conocidos de HTML\r\nhdeleon.net\r\n•\r\n8,5 K visualizaciones • hace 8 meses\r\n\r\n\r\n30:30\r\nREPRODUCIENDO\r\n¿Por qué existe la criptografía? - Luis Argerich\r\nnerdearla\r\n•\r\n388 visualizaciones • hace 8 meses\r\n\r\n\r\n14:46\r\nREPRODUCIENDO\r\n5 conceptos de REACT AVANZADO para ser MEJOR PROGRAMADOR\r\nCarlos Azaustre - Aprende JavaScript\r\n•\r\n14 K visualizaciones • hace 8 meses\r\n\r\n\r\n6:35\r\nREPRODUCIENDO\r\nMeal Prep For The Week In Under An Hour | Sweet and Sour Chicken\r\nChef Jack Ovens\r\n•\r\n4,1 M de visualizaciones • hace 7 meses\r\n\r\n\r\n21:40\r\nREPRODUCIENDO\r\n¡EL PC Gaming con HOLOGRAMAS!\r\nNate Gentile\r\n•\r\n920 K visualizaciones • hace 7 meses\r\n\r\n\r\n7:39\r\nREPRODUCIENDO\r\n5 Patrones de Diseño en FRONTEND\r\nhdeleon.net\r\n•\r\n9,5 K visualizaciones • hace 7 meses\r\n\r\n\r\n23:31\r\nREPRODUCIENDO\r\nChatGPT - el Hype, los Desafíos y el Futuro\r\nDot CSV\r\n•\r\n930 K visualizaciones • hace 7 meses\r\n\r\n\r\n13:46\r\nREPRODUCIENDO\r\nJavaScript Avanzado, Cancelación de Promesas 🤘\r\nhdeleon.net\r\n•\r\n6,8 K visualizaciones • hace 7 meses\r\n\r\n\r\n9:53\r\nREPRODUCIENDO\r\nQué aprender para ser programador backend el 2023?\r\nHolaMundo\r\n•\r\n157 K visualizaciones • hace 6 meses\r\n\r\n\r\n14:49\r\nREPRODUCIENDO\r\n💥Warren Buffett: \"Cómo debes invertir en 2023\"\r\nInvierte y gana\r\n•\r\n300 K visualizaciones • hace 6 meses\r\n\r\n\r\n8:50\r\nREPRODUCIENDO\r\nASÍ TE ENGAÑAN LOS YOUTUBERS Y LAS MARCAS\r\nMatt Yutoshi\r\n•\r\n13 K visualizaciones • hace 6 meses\r\n\r\n\r\n22:05\r\nREPRODUCIENDO\r\nANTIRROBO E INDESTRUCTIBLES!!\r\nSupraPixel\r\n•\r\n108 K visualizaciones • hace 6 meses\r\n\r\n\r\n1:41:55\r\nREPRODUCIENDO\r\n¡¡Descubriendo Nuevas Funciones de Notion AI contigo!!\r\nProductividad con Ruben Loan\r\n•\r\n4,1 K visualizaciones • Emitido hace 6 meses\r\n\r\n\r\n29:12\r\nREPRODUCIENDO\r\nGANÉ UN PREMIO ESLAND 😎\r\nJuanSGuarnizo\r\n•\r\n1,4 M de visualizaciones • hace 5 meses\r\n\r\n\r\n31:02\r\nREPRODUCIENDO\r\nTengen Uzui's Nichirin Blades : [Demon Slayer] Season 2\r\nThat Works\r\n•\r\n1,5 M de visualizaciones • hace 5 meses\r\n\r\n\r\n34:15\r\nREPRODUCIENDO\r\nEste es el trabajo de 2 AÑOS... STAR WARS BB8 PC\r\nNate Gentile\r\n•\r\n1,3 M de visualizaciones • hace 5 meses\r\n\r\n\r\n16:01\r\nREPRODUCIENDO\r\n😍 Juegos PC de POCOS REQUISITOS de 2023 (sin placa de video) + LINKS\r\nEdu Camps\r\n•\r\n50 K visualizaciones • hace 4 meses\r\n\r\n\r\n38:49\r\nREPRODUCIENDO\r\nEstoy HARTO de esto\r\nSupraPixel\r\n•\r\n46 K visualizaciones • hace 4 meses\r\n\r\n\r\n9:59\r\nREPRODUCIENDO\r\n7 DESUBICADOS JUEGOS INDIE de MARZO | Lanzamientos Juegos Indie PC\r\nEdu Camps\r\n•\r\n19 K visualizaciones • hace 4 meses\r\n\r\n\r\n19:55\r\nREPRODUCIENDO\r\nTodas las empresas están lanzando su propia IA ¿cómo lo hacen?\r\nEDteam\r\n•\r\n31 K visualizaciones • hace 4 meses\r\n\r\n\r\n1:53:27\r\nREPRODUCIENDO\r\n#VAMOSVIENDO | ARMAMOS LA FUTURA FAMILIA DEL MOMO EN VIVO!\r\nPiso 18\r\n•\r\n29 K visualizaciones • hace 4 meses\r\n\r\n\r\n50:45\r\nREPRODUCIENDO\r\n¿Cómo funciona ChatGPT? La revolución de la Inteligencia Artificial\r\nNate Gentile\r\n•\r\n3,9 M de visualizaciones • hace 4 meses\r\n\r\n\r\n36:13\r\nREPRODUCIENDO\r\nPOR ESTO ARGENTINA PODRÍA PERDER LA ANTARTIDA EN 2048\r\nMomo\r\n•\r\n370 K visualizaciones • hace 4 meses\r\n\r\n\r\n23:19\r\nREPRODUCIENDO\r\nHe probado GPT-4 para Programar en C# .NET\r\nhdeleon.net\r\n•\r\n84 K visualizaciones • hace 4 meses\r\n\r\n\r\n24:40\r\nREPRODUCIENDO\r\nCHARLANDO con ROSALÍA y RAUW ALEJANDRO\r\nIbai\r\n•\r\n4,2 M de visualizaciones • hace 3 meses\r\n\r\n\r\n1:37:12\r\nREPRODUCIENDO\r\nCAZZU EN FERNÉ CON GREGO\r\nGrego Rossello\r\n•\r\n188 K visualizaciones • hace 3 meses\r\n\r\n\r\n21:37\r\nREPRODUCIENDO\r\nREVIEW: Cocinamos en la Primera parilla a Gas - Resultado Impactante | Locos X el Asado\r\nLocos X el Asado\r\n•\r\n240 K visualizaciones • hace 3 meses\r\n\r\n\r\n1:01:48\r\nREPRODUCIENDO\r\nLike & DIslike: Final Fantasy XVI, Redfall, Minecraft Legends...\r\nEurogamerspain\r\n•\r\n32 K visualizaciones • hace 3 meses\r\n\r\n\r\n19:29\r\nREPRODUCIENDO\r\nRoadmap BACKEND DEV 🤘\r\nhdeleon.net\r\n•\r\n13 K visualizaciones • hace 3 meses\r\n\r\n\r\n2:38:33\r\nREPRODUCIENDO\r\nLa Historia de la Humanidad con sus episodios más trascendentales\r\nAcademia Play\r\n•\r\n1,6 M de visualizaciones • hace 2 meses\r\n\r\n\r\n47:10\r\nREPRODUCIENDO\r\nLike & Dislike: Zelda Tears of the Kingdom, Dead Island 2, Horizon Forbbiden West: Burning Shores...\r\nEurogamerspain\r\n•\r\n35 K visualizaciones • hace 2 meses\r\n\r\n\r\n15:05\r\nREPRODUCIENDO\r\nEl desafío de la escalabilidad: Monolitos vs. Microservicios (PRIME VIDEO VUELVE A MONOLITOS)\r\nPelado Nerd\r\n•\r\n49 K visualizaciones • hace 2 meses\r\n\r\n\r\n38:32\r\nREPRODUCIENDO\r\nHa superado TODA expectativa... El PC de Ibai\r\nNate Gentile\r\n•\r\n2,2 M de visualizaciones • hace 1 mes\r\n\r\n\r\n10:26\r\nREPRODUCIENDO\r\nWindows 11 se puso ZARPADO\r\nSupraPixel\r\n•\r\n111 K visualizaciones • hace 1 mes\r\n\r\n\r\n8:11\r\nREPRODUCIENDO\r\n¿Qué Diablos es PROGRAMACIÓN CONCURRENTE?\r\nhdeleon.net\r\n•\r\n8,1 K visualizaciones • hace 1 mes\r\n\r\n\r\n9:56\r\nREPRODUCIENDO\r\nAsync/await: cuándo usarlo y cuándo no\r\nCodelyTV - Redescubre la programación\r\n•\r\n8 K visualizaciones • hace 1 mes\r\n\r\n\r\n1:33:27\r\nREPRODUCIENDO\r\n¿Qwik Remplazará a React? | Raw Radio #15 ft Leifer Mendez\r\nhdeleon.net\r\n•\r\n5,9 K visualizaciones • hace 1 mes\r\n\r\n\r\n18:29\r\nREPRODUCIENDO\r\nJohn Wick, La Mejor Pelicula De Accion Del Año | #TeLoResumo\r\nTe lo resumo\r\n•\r\n1,4 M de visualizaciones • hace 1 mes\r\n\r\n\r\n17:12\r\nREPRODUCIENDO\r\n¡SQL AVANZADO! COMMON TABLE ESPRESSIONS\r\nhdeleon.net\r\n•\r\n5,5 K visualizaciones • hace 3 semanas\r\n\r\n\r\n1:09:31\r\nREPRODUCIENDO\r\nSobre la encuesta de Stack Overflow 2023… | #laFunción 8x36\r\nCodelyTV - Redescubre la programación\r\n•\r\n19 K visualizaciones • Emitido hace 2 semanas\r\n\r\n\r\n1:46:18\r\nREPRODUCIENDO\r\n#CONLOJUSTO | CONOCIENDO A ACRU!\r\nPiso 18\r\n•\r\n31 K visualizaciones • hace 11 días\r\n\r\n\r\n25:01\r\nREPRODUCIENDO\r\nJWT para Programar Backend Seguro\r\nhdeleon.net\r\n•\r\n12 K visualizaciones • hace 9 días\r\n\r\n\r\n8:05\r\nREPRODUCIENDO\r\nYo SÍ Iría a La Universidad\r\nBettaTech\r\n•\r\n19 K visualizaciones • hace 6 días";
            var videosArr = videos.Split("\r\n\r\n\r\n");
            var lista = new List<Video>();
            int totalSeconds = 0;

            foreach (var v in videosArr)
            {
                var obj = v.Split("\r\n");
                var duration = obj[0].Trim();
                var durationArr = duration.Split(":");
                int sec = durationArr.Count() == 3
                    ? Int16.Parse(durationArr[0]) * 3600 + Int16.Parse(durationArr[1]) * 60 + Int16.Parse(durationArr[2])
                    : Int16.Parse(durationArr[0]) * 60 + Int16.Parse(durationArr[1]);
                string publicacion = obj.Last().Split(" • ").Last();
                var video = new Video { Duracion = sec, Titulo = obj[2], Autor = obj[3], CuandoSePublico = publicacion };
                lista.Add(video);
            }
            List<Video> videosCantidad = new List<Video>();
            foreach (var item in lista)
            {
                item.Cantidad = lista.Where(x => x.Autor == item.Autor).Count();
                videosCantidad.Add(item);
            }

            videosCantidad.ForEach(item => totalSeconds += item.Duracion);
            var totalTime = GetTime(totalSeconds);

           LogConsole(totalTime);
           int cantDias = GetFianalizacionDeDias(totalSeconds,2);
            
           return videosCantidad.OrderByDescending(x => x.Cantidad).ToList();
        }

        private static int GetFianalizacionDeDias(int totalSeconds,int cantHorasDias)
        {
            int cantDias = 0;
            var hor = totalSeconds / 3600;
            while (hor >= 0)
            {
                cantDias++;
                hor -= cantHorasDias;
            }
            LogConsole($"\nDedicando {cantHorasDias}hs por dia cuanto tardaria en finalizar todo?\n");
            LogConsole("\nfinalizarias en " + cantDias + " Dias\n");
            return cantDias;
        }

        private static void LogConsole(string mensaje)
        {
           LogConsole($"\r\n{mensaje}\r\n");
        }
        #endregion
    }
}