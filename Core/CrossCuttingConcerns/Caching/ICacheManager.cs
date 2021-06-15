using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching
{
    interface ICacheManager
    {            //key'e karşılık gelen bir veri döndür.
        T Get<T>(string key); //cache'den dönen veri list te olabilir tek bir satır da olabilir. Bunları dikkate alarak generic bir metod yazıyoruz.
        object Get(string key);
        void Add(string key, object value,int duration);//duration cache'de durma süresi
        bool IsAdd(string key);//cache'den mi gelsin veritabanından mı? yani cachede bu veri var mı git bak
        void Remove(string key);//cache'den uçurma metodu
        void RemoveByPattern(string pattern);//regex(regular expression ile metod ismi içinde 'Get' , 'Category' vs gibi isim olanları sil)
    }
}
