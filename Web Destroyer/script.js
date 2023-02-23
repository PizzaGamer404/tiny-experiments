javascript: (function() { /*If you paste this into a bookmark's link, it will run this when you click it. (It doesn't work on the new tab page.)*/
    var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 "; /*used to generate random characters*/
    var text = function(original) { /*changes 10% of characters in a string to random ones*/
        let newText = "";
        for (let i = 0; i < original.length; i++) {
            if (Math.random() > 0.9) newText += chars[Math.floor(Math.random() * chars.length)];
            else newText += original[i];
        }
        return newText;
    };
    var elements; /*defines elements, which will be set to all elements in the page*/
    var count = 0; /*count is basically the strength of the effect, which goes up over time*/
    var chaos = function() { /*this is the main effect*/
        var e = elements[Math.floor(Math.random() * elements.length)]; /*get a random element*/
        /*randomize its color*/
        e.style.color = "rgb(" + Math.floor(Math.random() * 255) + ", " + Math.floor(Math.random() * 255) + ", " + Math.floor(Math.random() * 255) + ")";
        /*randomizes its background color*/
        e.style.backgroundColor = "rgb(" + Math.floor(Math.random() * 255) + ", " + Math.floor(Math.random() * 255) + ", " + Math.floor(Math.random() * 255) + ")";
        /*mess with the text (if any)*/
        let cn = e.childNodes[0];
        if (cn && cn.nodeValue) cn.nodeValue = text(cn.nodeValue);
    };
    setInterval(function() { /*runs ~100 times per second*/
        elements = document.body.getElementsByTagName("*"); /*gets all elements on the page*/
        /*small chance to increase the count, chance increases as count increases*/
        /*with count of 0, it is 1%, with count of 3000, it is 100%, linearly increasing*/
        /*this makes it go faster and faster*/
        if (Math.random() > 0.99 - count / 3000 * 0.99) count++;
        for (var i = 1; i <= count / 100; i++) { /*for 1/100 of the count, run chaos()*/
            chaos();
        }
    }, 10);
})();
