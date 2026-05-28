(function () {
    const optionalImages = document.querySelectorAll("[data-optional-image]");
    const copyButtons = document.querySelectorAll("[data-copy-target]");
    const navLinks = document.querySelectorAll(".topnav a, .sidebar a");
    const sections = Array.from(document.querySelectorAll("section[id]"));

    optionalImages.forEach((image) => {
        if (image.complete) {
            if (image.naturalWidth > 0) {
                image.closest("figure")?.classList.add("is-loaded");
            } else {
                image.remove();
            }

            return;
        }

        image.addEventListener("load", () => {
            image.closest("figure")?.classList.add("is-loaded");
        });

        image.addEventListener("error", () => {
            // 截图未放入目录时移除破图图标, 保留拍摄说明占位.
            image.remove();
        });
    });

    copyButtons.forEach((button) => {
        button.addEventListener("click", async () => {
            const targetId = button.getAttribute("data-copy-target");
            const target = targetId ? document.getElementById(targetId) : null;
            if (!target) {
                return;
            }

            try {
                // 复制代码时保留原始换行, 方便直接粘贴到面试材料.
                await navigator.clipboard.writeText(target.innerText);
                const previousText = button.textContent;
                button.textContent = "已复制";
                window.setTimeout(() => {
                    button.textContent = previousText;
                }, 1200);
            } catch (error) {
                button.textContent = "复制失败";
                window.setTimeout(() => {
                    button.textContent = "复制";
                }, 1200);
            }
        });
    });

    if ("IntersectionObserver" in window) {
        const observer = new IntersectionObserver((entries) => {
            entries.forEach((entry) => {
                if (!entry.isIntersecting) {
                    return;
                }

                const id = entry.target.getAttribute("id");
                navLinks.forEach((link) => {
                    link.classList.toggle("is-active", link.getAttribute("href") === `#${id}`);
                });
            });
        }, {
            rootMargin: "-32% 0px -55% 0px",
            threshold: 0.01
        });

        sections.forEach((section) => observer.observe(section));
    }
})();
