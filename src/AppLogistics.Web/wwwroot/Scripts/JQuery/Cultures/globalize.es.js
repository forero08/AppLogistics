window.cultures = window.cultures || {};
window.cultures.globalize = window.cultures.globalize || {};
window.cultures.globalize['es'] = {
    name: "es",
    englishName: "Spanish (Colombia)",
    nativeName: "Español (Colombia)",
    language: "es",
    numberFormat: {
        ",": ".",
        ".": ",",
        negativeInfinity: "-Infinito",
        positiveInfinity: "Infinito",
        percent: {
            pattern: ['-n%', 'n%'],
            ",": ".",
            ".": ","
        },
        currency: {
            pattern: ['-$n', '$n'],
            ",": ".",
            ".": ",",
            symbol: '$'
        }
    },
    calendars: {
        standard: {
            '/': '.',
            firstDay: 1,
            days: {
                names: ["domingo", "lunes", "martes", "miércoles", "jueves", "viernes", "sábado"],
                namesAbbr: ["dom", "lun", "mar", "mié", "jue", "vie", "sáb"],
                namesShort: ["do", "lu", "ma", "mi", "ju", "vi", "sá"]
            },
            months: {
                names: ["enero", "febrero", "marzo", "abril", "mayo", "junio", "julio", "agosto", "septiembre", "octubre", "noviembre", "diciembre", ""],
                namesAbbr: ["ene", "feb", "mar", "abr", "may", "jun", "jul", "ago", "sep", "oct", "nov", "dic", ""]
            },
            AM: ["a.m.", "a.m.", "A.M."],
            PM: ["p.m.", "p.m.", "P.M."],
            patterns: {
                d: "dd/MM/yyyy",
                D: "dddd, dd' de 'MMMM' de 'yyyy",
                t: "hh:mm tt",
                T: "hh:mm:ss tt",
                f: "dddd, dd' de 'MMMM' de 'yyyy hh:mm tt",
                F: "dddd, dd' de 'MMMM' de 'yyyy hh:mm:ss tt",
                M: "dd MMMM",
                Y: "MMMM' de 'yyyy"
            }
        }
    }
};