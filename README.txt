# Cascabel
## Antes de empezar
Debe instalar la extensión de Spec flow en Visual Studio, para ello deberá seguir el siguiente instructivo: Install Visual Studio extension — documentation (specflow.org)

Verifique la version de Google Chrome con el Nuget de Selenuim para el controlador del explorador.

## Framework y librerías
- Para el desarrollo de la automatización se utilizará las siguientes librerías:
- Selenium.WebDriver
- Selenium.WebDriver.ChromeDriver
- Selenium.Support
- SpecFlow
- SpecRun.Runner
- SpecRun.SpecFlow
- ExtentReports
- FluentAssertions
- Boa.Constrictor

**NOTA** : Todas estas librerías funcionarán bajo .NET Framework 4.7.2

## Arquitectura

### Patrón Screenplay 
- Un actor hace preguntas sobre el estado de los elementos en una pantalla.
- Un actor realiza una(s) tarea(s) compuesta por acciones que interactúan con elementos en una pantalla.
- Un actor tiene habilidades para habilitar acciones que interactúan con elementos en una pantalla.

![Shema](https://lh3.googleusercontent.com/pw/AM-JKLVi9L3hWCkstbjcAqtlaeKpTv8aKkTMKHrzX5Ec2G8CgY5THvBkG5yi3u_cOXd88mBJwzmJzKAmn6bVp6o1IhaJb7wSn_NiPimiKP01Oa68-7f2_CBWl7WRiquDgAc3NpplKWnLcDnAzyBnMqb5i1vq=w744-h531-no?authuser=0)

Teniendo en cuenta el patrón de Screenplay (que se detalló en el primer grafico, aquí se detalla la arquitectura de la aplicación desde los componentes. De color Amarillo se observa el objeto Feature el cual recopila el Guión de los actores, aquí es donde comienza a codificar el Automatizador con documentación viva por medio de la sintaxis Gherkins, esta sintaxis funciona bajo el esquema Dado (given) un escenario “X” Cuando (when) se se realice “Y” entonces (Then) “Z” pasará. 

**Ejemplo:**

Feature: Example
Framework example

@Functional

**Scenario:** Envío de formulario exitoso
**Given** 
El usuario se encuentra en el formulario web ubicado en <url>
**And** ingrese el primer nombre '<firstName>', el segundo nombre '<middleName>' y el apellido '<lastName>'
**When** hace clic en la opción de enviar
**Then** el usuario verá el mensaje de bienvenida '<message>'
**And** el navegador web se cerrará

Examples: 
| url| firstName | middleName | lastName | message |
| https://form.jotform.com/212346521647656 | Cristian  | Camilo| Moreno| Thank You!|

Este lenguaje debe estar acompañado de una serie de pasos (steps) los cuales deben ser programables desde C#, estos pasos deben ser configurados en una clase que herede de SpecFlowSteps para que el framework funcione. Dichos  pasos están orientados a representar las acciones de un actor en un escenario particular en una pantalla especifica, por lo que se debe construir una clase Pagina (page) que complemente las acciones de los actores en el escenario.

## Empezar
### Creación de la solución desde cero
- Instale la extensión de SpectFlow para su visual estudio (este manual se detalla para VS 2017), siguiendo aqui.
- Cree una solución de tipo UnitTest (.Net Framework 4.61).
-Instale los nugets citados con anterioridad, si ha seguido este manual deberá contar con unos archivos como estos en su proyecto:
    - runtest.cmd
    - SpecRunTestProfile.xsd
    - SpecRunTestProfile_2011_09.xsd
- Cree una carpeta llamada Features, otra llamada Pages y otra Steps

### Creación de un feature nuevo dentro del Framework
- En la carpeta Feature agregue un nuevo elemento y seleccione del apartado de SpectFlow un Feaure.
- Aquí deberá construir los diferentes escenarios de acuerdo a cada feature en lenguaje Gherkins. Cuando escriba las diferentes acciones se irán colocando en color violeta , esto quiere decir que no existe una acción desde C# que pueda ejecuta tal operación (esto es normal).
- Una vez termine de crear un feature en lenguaje Gherkins, proceda a hacer click derecho en el feature y a seleccionar **Generate Step Definitions**, del menú contextual (si no le sale, es por que falto instalar un nuget) y ubíquelo dentro de la carpeta **Steps**. 
- Esta  operación hará que las acciones cambien de color violeta a color blanco.
- Herede el step de **BaseStep** y cree le constructor vacío 
- Dentro de la carpeta **Pages**, Agregue la información de los elementos de la pagina para que el frameweork de Boa.Constrictor funcione correctamente, para ello deberá utilizar la interface IWebLocator, agregar una descripción y el modo de buscarlo (en la manera de lo posible utilice el ID).

>**Ayuda:** Puede usar el plugin de selenio para su explorador para que le asista con los Xpath

**Importante:**
- La clausula `actor.AttemptsTo` indica que el actor intenta realizar una acción.
- La clausula `_actor.AsksFor` indica que se esta preguntando si el actor debe ver algo.
- Todos los casos donde algo no este en su lugar o no corresponda, debe romper por medio de una excepción para que el framework reaccione y sepa que el caso de prueba falló. la mejor manera de romper es: 

```C#
catch (Exception ex)
{
    InsterStepIntoReport(Driver, _scenarioContext, enabledS3: _enabledS3, ex: ex);
    throw ex;
}
```

## ¿Cómo hago para agregar un escenario a un feature existente?
- Cuando se crea un escenario nuevo en un  feature existente, las acciones se tornarán de color violeta.
- Una vez identifique la acción, haga click derecho en la acción y seleccione **Go to Step Definition**
- Al no existir, SpecFlow le preguntará su quiere que le asista para crear un “esqueleto” del método para luego pegarlo en el portapapeles, a lo que escogeremos la opción **SI**
- Diríjase al archivo Step respectivo y pegue lo que tiene en el porta papeles. 

## Licencia
MIT

**Free Software, Hell Yeah!**