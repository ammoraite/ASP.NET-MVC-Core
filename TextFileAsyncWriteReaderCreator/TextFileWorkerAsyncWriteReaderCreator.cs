using System.Text;

namespace TextFileAsyncWriteReaderCreator
{
    public class TextFileWorkerAsyncWriteReaderCreator
    {
        public delegate void FileWorkerMethodContainer ( );

        /// <summary>
        /// Событие завершения работы метода "ReadStringsInTextFilesAsync"
        /// </summary>
        public event FileWorkerMethodContainer? CompleateReadStringsInTextFilesAsyncEvent;

        /// <summary>
        /// Событие завершения работы метода "CreateFilesAsync"
        /// </summary>
        public event FileWorkerMethodContainer? CompleateCreateFilesAsyncEvent;

        /// <summary>
        /// Событие завершения работы метода "WriteTextInTextFileAsync"
        /// </summary>
        public event FileWorkerMethodContainer? CompleateWriteTextInTextFileAsyncEvent;

        /// <summary>
        /// результат работы "ReadStringsInTextFilesAsync"
        /// </summary>
        private string[]? _resultReadStringsInTextFilesAsync;

        /// <summary>
        /// readonly ссылка на результат работы "ReadStringsInTextFilesAsync"
        /// </summary>
        public ref readonly string[]? resultReadStringsInTextFilesAsync => ref _resultReadStringsInTextFilesAsync;

        /// <summary>
        /// Считывает асихронно все строки из текстовых файлов,
        /// полные адреса которых переданы через параметр "fileFullNames". 
        /// доступ к результату осуществляется через поле "resultReadStringsInTextFilesAsync"
        /// </summary>
        /// <param name="fileFullNames">Полные адреса текстовых файлов</param>
        /// <returns></returns>
        public async Task ReadStringsInTextFilesAsync ( params string[] fileFullNames )
        {
            await Task.Run (async ( ) =>
            {
                List<string> filesTekst = new ( );
                foreach (var item in fileFullNames)
                {
                    if (File.Exists (item))
                    {
                        StringBuilder sringbuilder = new ( );

                        using (var streamReader = new StreamReader (item))
                        {
                            do
                            {
                                var a = await streamReader.ReadLineAsync ( );

                                if (string.IsNullOrEmpty (a))
                                {
                                    break;
                                }
                                if (sringbuilder.ToString ( ).Length>0)
                                {
                                    sringbuilder.Append (a).Append ('\n');
                                }
                                sringbuilder.Append (a);

                            } while (true);
                        }
                        filesTekst.Add (sringbuilder.ToString ( )??string.Empty);
                    }
                }
                _resultReadStringsInTextFilesAsync=filesTekst.ToArray ( );
                if (CompleateReadStringsInTextFilesAsyncEvent is not null)
                {
                    CompleateReadStringsInTextFilesAsyncEvent.Invoke ( );
                }
            });
        }

        /// <summary>
        /// Создает асихронно файлы 
        /// </summary>
        /// <param name="fileInfos"></param>
        /// <returns></returns>
        public async Task CreateFilesAsync ( params FileInfo[] fileInfos )
        {
            await Task.Run (( ) =>
            {
                foreach (var file in fileInfos)
                {
                    if (!File.Exists (file.Name))
                    {
                        file.Create ( );
                    }
                }
                if (CompleateCreateFilesAsyncEvent is not null)
                {
                    CompleateCreateFilesAsyncEvent.Invoke ( );
                }
            });
        }

        /// <summary>
        /// Записывает строки асихронно в текстовые файлы
        /// </summary>
        /// <param name="fileInfosAndText">Кортежи, каждый содержит :"FileInfo"-информация о файле в который нужно произвести запись
        /// "string"-срока которая будет записана "FileMode"-модификатор записи-чтения</param>
        /// <returns></returns>
        public async Task WriteTextInTextFileAsync ( params (FileInfo, string, FileMode)[] fileInfosAndText )
        {
            await Task.Run (async ( ) =>
            {
                foreach (var file in fileInfosAndText)
                {
                    using StreamWriter sw = new (new FileStream (file.Item1.FullName, file.Item3));
                    await sw.WriteLineAsync (file.Item2);
                }
                if (CompleateWriteTextInTextFileAsyncEvent is not null)
                {
                    CompleateWriteTextInTextFileAsyncEvent.Invoke ( );
                }
            });
        }
    }
}