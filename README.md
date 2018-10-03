# Parcial3_Base

Juan David Atehortúa Loaiza
ID: 000312318

Explicación del problema:
En un primer momento el método MustAllChildrenSucced hacía que simpre se devolviera falso, por lo tanto, Selectors y
Sequences nunca iban a recibir "luz verde" para la ejecución de una rama del árbol específica.
Para rematar, Selectors y Sequences, dado a que son hijos del Composite, tenían el mismo comportamiento, ninguno hacía override
de la funcionalidad de Composite, por lo que funcionaban "igual". Para esto era necesario diferenciar los comportamientos
de ambos, es decir, el Sequence debe ejecutar todos sus hijos en orden y el Selector solo uno de ellos (&& y || respetivamente).
